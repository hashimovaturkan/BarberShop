using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Exceptions;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.User.Queries.UserLogin;
using BarberShop.Application.EntityCQ.Admin.Commands;
using BarberShop.Application.EntityCQ.Admin.Interfaces;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.EntityCQ.User.Queries.UserList;
using BarberShop.Application.Enums;
using BarberShop.Application.Models.Dto.Mail;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Admin;
using BarberShop.Application.Models.Vm.Roles;
using BarberShop.Application.Models.Vm.User;
using BarberShop.Application.Repos;
using BarberShop.Domain;
using BarberShop.Persistence;
using IntraNet.Application.Models.Vm.User;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Admin.Services
{
    public class AdminService : IAdminService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly UserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly UriService _uriService;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        readonly IHttpContextAccessor httpContextAccessor;

        public AdminService(IHttpContextAccessor httpContextAccessor, UserRepo userRepo, BarberShopDbContext dbContext, IMapper mapper, UriService uriService,
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

        public async Task<int> CreateAsync(CreateAdminCommand request)
        {
            var user = await _dbContext.Admins.FirstOrDefaultAsync(x => x.Phone == request.Phone && x.IsActive);

            if (user is not null)
                throw new BadRequestException("User is exist");


            var salt = Guid.NewGuid();
            string passHash = AesOperation.Encrypt(request.Password);

            user = new Domain.Admin
            {
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                Password = passHash,
                Role = request.Role,
                IsActive = true,
                CreatedIp = request.UserIp
            };


            await _dbContext.Admins.AddAsync(user);

            await _dbContext.SaveChangesAsync(CancellationToken.None);


            return user.Id;
        }

        public async Task<AdminDetailsVm> Get(int userId)
        {
            var user = await _dbContext.Admins.FirstOrDefaultAsync(e => e.Id == userId && e.IsActive);
            if (user == null)
                throw new NotFoundException(nameof(Admin), userId);

            var vm = _mapper.Map<AdminDetailsVm>(user);

            if (user.PhotoId != null)
                vm.ImageUrl = httpContextAccessor.GeneratePhotoUrl((int)user.PhotoId);

            return vm;
        }

        public async Task<List<RoleVm>> GetRoles()
        {
            var response = new List<RoleVm>()
            {
                new RoleVm(){RoleId = (int)Roles.SuperAdmin, RoleName = Roles.SuperAdmin.ToString()},
                new RoleVm(){RoleId = (int)Roles.Admin, RoleName = Roles.Admin.ToString()},
                new RoleVm(){RoleId = (int)Roles.Editor, RoleName = Roles.Editor.ToString()},

            };

            return response;

        }

        public async Task<AdminLoginVm> LoginAsync(UserLoginQuery request)
        {
            var user = await _dbContext.Admins
                .FirstOrDefaultAsync(e => e.Phone == request.Phone && e.IsActive,
                    CancellationToken.None);

            if (user is null)
                throw new UnauthorizedException($"User not exist");

            string passHash = AesOperation.Decrypt(user.Password);

            if (request.Password != passHash)
                throw new UnauthorizedException($"User's password isn't correct");

            AdminLoginVm userLoginVm = _mapper.Map<AdminLoginVm>(user);

            userLoginVm.LangId = 1;

            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return userLoginVm;
        }

        public async Task<int> Update(UpdateAdminCommand userDto)
        {
            var user = await _dbContext.Admins.FirstOrDefaultAsync(e => e.Id == userDto.Id && e.IsActive);
            if (user == null)
                throw new NotFoundException(nameof(Admin), userDto.Id);

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
    }
}
