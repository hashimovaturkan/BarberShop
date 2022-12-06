﻿using AutoMapper;
using BarberShop.Application.Common.Exceptions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntityCQ.Gift.Commands.GiftOrder;
using BarberShop.Application.EntityCQ.Gift.Interfaces;
using BarberShop.Application.Models.Dto.Gift;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Application.Repos;
using BarberShop.Domain;
using BarberShop.Persistence;
using BarberShop.Persistence.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BarberShop.Application.EntityCQ.Gift.Services
{
    public class GiftService : IGiftService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly UserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly UriService _uriService;
        public GiftService(UserRepo userRepo,BarberShopDbContext dbContext, IMapper mapper, UriService _uriService)
        {
            this._dbContext = dbContext;
            this._userRepo = userRepo;
            this._mapper = mapper;
            this._uriService = _uriService;
        }

        public async Task<List<GiftListDto>> GetList()
        {
            var gifts = await _dbContext.Gifts.Where(e => e.IsActive).ToListAsync();

            var vm = _mapper.Map<List<GiftListDto>>(gifts);

            return vm;
        }

        public async Task<List<OrderGiftListVm>> GetOrderGiftList(int userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            var vm = _mapper.Map<List<OrderGiftListVm>>(user.UserGiftRelations);

            return vm;
        }

        public async Task<bool> OrderGift(OrderGiftCommand command)
        {
            var user = await _userRepo.GetUserByIdAsync(command.UserId);
            var gift = await _dbContext.Gifts.FirstOrDefaultAsync(e => e.Id == command.GiftId);

            if (user == null)
                throw new UnauthorizedException("User not found");

            if (gift == null)
                throw new BadRequestException("Gift not found");

            if (user.UserBonuses >= gift.Price)
            {
                user.UserBonuses = user.UserBonuses - (int)gift.Price; 

                var userGift = new UserGiftRelation() { UserId = command.UserId, CreatedIp = command.UserIp, GiftId = command.GiftId, Status = false };

                await _dbContext.UserGiftRelations.AddAsync(userGift);

                await _dbContext.SaveChangesAsync(CancellationToken.None);

                
            }

            return true;


        }
    }
}
