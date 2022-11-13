using AutoMapper;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntityCQ.AdditinalService.Interfaces;
using BarberShop.Application.Models.Vm.AdditionalService;
using BarberShop.Application.Models.Vm.Service;
using BarberShop.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.AdditinalService.Services
{
    public class AdditionalServiceService : IAdditionalServiceService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UriService _uriService;
        public AdditionalServiceService(BarberShopDbContext dbContext, IMapper mapper, UriService uriService)
        {
            this._uriService = uriService;
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<List<AdditionalServiceLookupDto>> Get()
        {
            var filials = await _dbContext.AdditionalServices.Where(e => e.IsActive).ToListAsync();

            var vm = _mapper.Map<List<AdditionalServiceLookupDto>>(filials);

            return vm;
        }
    }
}
