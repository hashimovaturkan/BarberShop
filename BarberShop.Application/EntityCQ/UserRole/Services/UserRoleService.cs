using AutoMapper;
using BarberShop.Application.Common.Exceptions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.Repos;
using BarberShop.Domain;
using BarberShop.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BarberShop.Application.EntitiesCQ.UserRole.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserRoleRepo _userRoleRepo;
        private readonly UserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly UriService _uriService;
        private readonly BarberShopDbContext _dbContext;
        private CancellationToken cancellationToken;

        public UserRoleService(UserRoleRepo userRoleRepo, UserRepo userRepo, IMapper mapper, UriService uriService, BarberShopDbContext dbContext)
            => ( _userRoleRepo, _userRepo, _mapper, _uriService, _dbContext) = ( userRoleRepo, userRepo, mapper, uriService, dbContext);

        public async Task AddRoleToUser(string name, int userId, string userIp)
        {
            var role = await _userRoleRepo.GetByNameAsync(name);
            
            if(role == null)
            {
                role = new Domain.UserRole
                {
                    Name = name,
                    CreatedIp = userIp,
                    Description = name,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                await _dbContext.UserRoles.AddAsync(role);

                await _dbContext.UserRoleRelations.AddAsync(new UserRoleRelation
                {
                    UserId = userId,
                    UserRole = role,
                    CreatedIp = userIp,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                });
            }
            else
            {
                await _dbContext.UserRoleRelations.AddAsync(new UserRoleRelation
                {
                    UserId = userId,
                    UserRoleId = role.Id,
                    CreatedIp = userIp,
                });
            }


            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
