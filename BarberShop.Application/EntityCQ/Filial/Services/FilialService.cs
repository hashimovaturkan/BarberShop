using AutoMapper;
using BarberShop.Application.EntityCQ.Filial.Interfaces;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Filial.Services
{
    public class FilialService : IFilialService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        public FilialService(BarberShopDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<List<FilialListDto>> GetList()
        {
            var filials = await _dbContext.Filials.Where(e => e.IsActive).ToListAsync();

            var vm = _mapper.Map<List<FilialListDto>>(filials);

            return vm;
        }
    }
}
