using AutoMapper;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntityCQ.Gift.Interfaces;
using BarberShop.Application.Models.Dto.Gift;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Gift.Services
{
    public class GiftService : IGiftService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UriService _uriService;
        public GiftService(BarberShopDbContext dbContext, IMapper mapper, UriService _uriService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._uriService = _uriService;
        }

        public async Task<List<GiftListDto>> GetList()
        {
            var gifts = await _dbContext.Gifts.Where(e => e.IsActive).ToListAsync();

            var vm = _mapper.Map<List<GiftListDto>>(gifts);

            return vm;
        }
    }
}
