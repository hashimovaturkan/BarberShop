using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Exceptions;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.User.Queries.UserLogin;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.EntityCQ.User.Queries.UserList;
using BarberShop.Application.Models.Dto.Bonus;
using BarberShop.Application.Models.Dto.Mail;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.User;
using BarberShop.Application.Repos;
using BarberShop.Domain;
using BarberShop.Persistence;
using BarberShop.Persistence.Migrations;
using IntraNet.Application.Models.Vm.User;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoreLinq;
using Stripe;
using System;
using System.IO;
using System.Web;
using Balance = BarberShop.Domain.Balance;
using Photo = BarberShop.Domain.Photo;

namespace IntraNet.Application.EntitiesCQ.User.Services
{
    public class UserService : IUserService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly UserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly UriService _uriService;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor,UserRepo userRepo,BarberShopDbContext dbContext, IMapper mapper, UriService uriService,
            IWebHostEnvironment environment, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _userRepo = userRepo;
            _uriService = uriService;
            _environment = environment;
            _configuration = configuration;
        }

        public async Task<UserClaimsVm> GetUserClaims(int userId)
        {
            var userRoleClaimsQuery = await _dbContext.Users
                .Include(e => e.UserRoleRelations)
                .ThenInclude(e => e.UserRole).ThenInclude(e => e.UserRoleClaims).ThenInclude(e => e.UserClaim)
                .Where(e => e.Id == userId && e.IsActive && e.PhoneVerification)
                .FirstOrDefaultAsync();

            UserClaimsVm userClaimsVm = _mapper.Map<UserClaimsVm>(userRoleClaimsQuery);

            return userClaimsVm;
        }

        public async Task<UserLoginVm> LoginAsync(UserLoginQuery request)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(e => e.Phone == request.Phone && e.IsActive && e.PhoneVerification,
                    CancellationToken.None);

            if (user is null)
                throw new UnauthorizedException($"User not exist");

            string passHash = AesOperation.Decrypt(user.Password);

            if ((request.Password + user.Salt.ToString()) != passHash)
                throw new UnauthorizedException($"User not exist");

            if(user.QrCodeId == null)
            {
                var url = user.Id.ToString().QrCodeGenerate(_environment);
                Photo photo = new()
                {
                    Name =  "qrcode.png",
                    Path = url,
                    CreatedDate = DateTime.UtcNow,
                    CreatedIp = "::1"
                };

                user.QrCode = photo;

                await _dbContext.Photos.AddAsync(photo);
            }
            
            UserLoginVm userLoginVm = _mapper.Map<UserLoginVm>(user);

            userLoginVm.LangId = 1;

            await _dbContext.UserLogs.AddAsync(new UserLog { UserId = user.Id, UserIp = request.UserIp, CreatedDate = DateTime.Now});            
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return userLoginVm;
        }

        public async Task<int> CreateAsync(CreateUserCommand request)
        {
            var user = await _dbContext.Users.Include(e => e.UserTokens).FirstOrDefaultAsync(x => x.Phone == request.Phone && x.IsActive);

            if (user is not null && user.PhoneVerification)
                throw new BadRequestException("User is exist"); 

            if (user is not null && !user.PhoneVerification)
            {
                Random random = new Random();
                var usertoken = user.UserTokens.First();
                usertoken.Value = random.Next(100000, 999999).ToString();

                await _dbContext.SaveChangesAsync(CancellationToken.None);
                var isSucces = await SmsVerification.SendSms(request.Phone.PhoneNumber(), usertoken.Value);

                if (isSucces)
                    return user.Id;

            }

            Random rnd = new Random();
            var userToken = new BarberShop.Domain.UserToken
            {
                Value =  rnd.Next(100000, 999999).ToString(),
                UserTokenTypeId = 1,
                UserTokenStatusId = 1,
                ExpireDate = DateTime.UtcNow.AddYears(1),
                CreatedIp = request.UserIp
            };

            if (user is not null && string.IsNullOrWhiteSpace(user.Password))
            {
                user.UserTokens.Add(userToken);
            }
            else
            {
                var salt = Guid.NewGuid();
                string passHash = AesOperation.Encrypt(request.Password + salt.ToString());

                user = new BarberShop.Domain.User
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Password = passHash,
                    Salt = salt,
                    FilialId = request.FilialId,
                    UserStatusId = 1,
                    IsActive = true,
                    CreatedIp = request.UserIp,
                    UserBonuses = 0,
                    Balance = new Balance() { CreatedIp = request.UserIp }


                };

                var isSucces = await SmsVerification.SendSms(request.Phone.PhoneNumber(), userToken.Value);

                if (isSucces)
                {
                    await _dbContext.Users.AddAsync(user);
                    user.UserTokens.Add(userToken);
                }

                
            }

            await _dbContext.SaveChangesAsync(CancellationToken.None);


            return user.Id;
        }

        public async Task<bool> VerifyPhoneNumber(string phoneNumber, string value)
        {
            var user = await _dbContext.Users.Include(e => e.UserTokens).FirstOrDefaultAsync(e => e.Phone == phoneNumber && e.IsActive);

            if (user != null)
            {
                if (user.UserTokens.FirstOrDefault()?.Value == value)
                {
                    user.PhoneVerification = true;

                    var isSucces = await _dbContext.SaveChangesAsync(CancellationToken.None);

                    return true;
                }
                else
                    return false;
            }

            return false;
        }

        public async Task<bool> SendSms(string phoneNumber)
        {
            var user = await _dbContext.Users.Include(e => e.UserTokens).FirstOrDefaultAsync(e => e.Phone == phoneNumber && e.IsActive);
            if( user != null)
            {
                Random rnd = new Random();
                var usertoken = user.UserTokens.First();
                usertoken.Value = rnd.Next(100000, 999999).ToString();

                await _dbContext.SaveChangesAsync(CancellationToken.None);
                var isSucces = await SmsVerification.SendSms(phoneNumber.PhoneNumber(), usertoken.Value);

                if (isSucces)
                    return true;
            }

            return false;
        }

        public async Task<int> ResetPassword(string phoneNumber, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Phone == phoneNumber && e.IsActive);
            if( user == null)
                throw new NotFoundException("User isn't exist");

            if (password != null)
            {
                string passHash = AesOperation.Encrypt(password + user.Salt.ToString());

                user.Password = passHash;

                await _dbContext.SaveChangesAsync(CancellationToken.None);

            }

            return user.Id;
        }

        public async Task<UserDetailsVm> Get(int userId)
        {
            var user = await _dbContext.Users.Include(e => e.Filial).Include(e => e.Balance).FirstOrDefaultAsync(e => e.Id == userId && e.IsActive);
            if (user == null)
                throw new NotFoundException(nameof(User), userId);

            var vm = _mapper.Map<UserDetailsVm>(user);

            if(user.PhotoId != null)
                vm.ImageUrl = httpContextAccessor.GeneratePhotoUrl((int)user.PhotoId);

            vm.QrCodeUrl = httpContextAccessor.GeneratePhotoUrl((int)user.QrCodeId);

            return vm;
        }

        public async Task<int> Update(UpdateUserCommand userDto)
        {
            var user = await _dbContext.Users.Include(e => e.Filial).FirstOrDefaultAsync(e => e.Id == userDto.Id && e.IsActive);
            if (user == null)
                throw new NotFoundException(nameof(User), userDto.Id);

            var imageId = user.PhotoId;

            _mapper.Map(userDto, user);
            user.UpdatedDate = DateTime.UtcNow.AddHours(4);
            user.PhotoId = imageId;

            if (userDto.Image != null)
            {
                Photo photo = new Photo();
                var image = userDto.Image.ConvertFile();

                photo = new()
                {
                    Name = image.File.FileName,
                    Path = image.Path,
                    CreatedDate = DateTime.UtcNow,
                    CreatedIp = "::1"
                };
                user.Photo = photo;
            }
                
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return user.Id;
        }

        public async Task<ResponseListTemplate<List<UserListVm>>> GetList(GetUserListQuery query)
        {
            var users = _dbContext.Users
                .Include(e => e.Filial)
                .Include(e => e.UserRoleRelations)
                .ThenInclude(e => e.UserRole)
                .Where(e => e.IsActive && e.UserRoleRelations.Select(k => k.UserRole.Name).Contains("User"));


            if (query.SearchingWord != null || query.SearchingWord != "" )
                users = users.Where(e => e.FullName.ToLower().Contains(query.SearchingWord.ToLower()));

            PaginationFilter paginationFilter = new PaginationFilter(query.Number, query.Size);
            IQueryable<BarberShop.Domain.User> carOrderRequestPagedQuery = paginationFilter.GetPagedList(users);

            int totalRecords = await users.CountAsync();

            var userList = _mapper.Map<List<UserListVm>>(await users.ToListAsync());

            foreach (var user in userList)
            {
                if(user.PhotoId != null)
                    user.ImageUrl = httpContextAccessor.GeneratePhotoUrl((int)user.PhotoId);
            }

            ResponseListTemplate<List<UserListVm>> result = userList.CreatePagedReponse(paginationFilter, totalRecords, _uriService, query.Route);

            return result;
        }

        public async Task<bool> SendMail(SendMailDto mailDto, int userId)
        {
            if (mailDto.Text == null)
                return false;

            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == userId);

            MailService mailService = new MailService(user.Email, _configuration);
            string mailBody = mailDto.Text;
            string mailSubject = user.FullName;
            var isSuccess = await mailService.SendMail(mailBody, mailSubject, CancellationToken.None);

            return isSuccess;

        }

        public async Task<int> UpdateBonus(UpdateBonusDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == dto.UserId);

            if (user == null)
                throw new NotFoundException(nameof(User), user.Id);

            user.UserBonuses = dto.UserBonuses;
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return user.Id;
        }
    }
}