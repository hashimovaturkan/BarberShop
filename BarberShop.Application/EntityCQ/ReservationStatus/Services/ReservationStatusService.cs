using AutoMapper;
using BarberShop.Application.EntityCQ.ReservationStatus.Interfaces;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Application.Models.Vm.ReservationStatus;
using BarberShop.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.ReservationStatus.Services
{
    public class ReservationStatusService : IReservationStatusService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        readonly IHttpContextAccessor httpContextAccessor;
        public ReservationStatusService(IHttpContextAccessor httpContextAccessor, BarberShopDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
        }

        public async Task<List<ReservationStatusListVm>> GetList()
        {
            var statuses = await _dbContext.ReservationStatuses.Where(e => e.IsActive).ToListAsync();

            var vm = _mapper.Map<List<ReservationStatusListVm>>(statuses);

            return vm;
        }
    }
}
