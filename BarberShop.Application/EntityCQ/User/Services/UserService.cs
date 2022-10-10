using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Exceptions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.User.Queries.PassResetGetUser;
using BarberShop.Application.EntitiesCQ.User.Queries.UserLogin;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.User;
using BarberShop.Domain;
using BarberShop.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BarberShop.Application.Models.Dto.User;
using MoreLinq;
using Microsoft.Extensions.Configuration;
using BarberShop.Application.Repos;

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

        public UserService(UserRepo userRepo,BarberShopDbContext dbContext, IMapper mapper, UriService uriService,
            IWebHostEnvironment environment, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
                throw new DefaultValidationException($"User not exist");

            string passHash = AesOperation.Decrypt(user.Password);

            if ((request.Password + user.Salt.ToString()) != passHash)
                throw new DefaultValidationException($"User not exist");
            
            UserLoginVm userLoginVm = _mapper.Map<UserLoginVm>(user);

            userLoginVm.LangId = 1;

            await _dbContext.UserLogs.AddAsync(new UserLog { UserId = user.Id, UserIp = request.UserIp, CreatedDate = DateTime.Now});            
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return userLoginVm;
        }

        public async Task<int> CreateAsync(CreateUserCommand request)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == request.Email && x.IsActive && x.PhoneVerification);

            if (user is not null && !string.IsNullOrWhiteSpace(user.Password))
                throw new Exception("User is exist");

            Random rnd = new Random();
            UserToken userToken = new UserToken
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
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Phone = request.Phone,
                    Password = passHash,
                    Salt = salt,
                    UserStatusId = 1,
                    IsActive = true,
                    CreatedIp = request.UserIp,
                    
                };

                List<UserFilial> userFilials = new List<UserFilial>();

                foreach (var id in request.FilialIds)
                {
                    userFilials.Add(new UserFilial
                    {
                        FilialId = id,
                        User = user,
                        CreatedIp= request.UserIp
                    });
                }

                user.UserFilials = userFilials;

                var isSucces = await SmsVerification.SendSms(request.Phone, userToken.Value);

                if (isSucces)
                {
                    await _dbContext.Users.AddAsync(user);
                    user.UserTokens.Add(userToken);
                }

            }

            await _dbContext.SaveChangesAsync(CancellationToken.None);


            return user.Id;
        }

        public async Task<bool> VerifyPhoneNumber(int? id, string value)
        {
            var user = await _userRepo.GetUserByIdAsync((int)id);

            if(user != null)
            {
                if (user.UserTokens.FirstOrDefault()?.Value == value)
                {
                    user.PhoneVerification = true;

                    await _dbContext.SaveChangesAsync(CancellationToken.None);

                    return true;
                }
                else
                    return false;
            }

            return false;
        }
    }
}