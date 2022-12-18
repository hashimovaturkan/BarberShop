using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Exceptions;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntityCQ.Balance.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Barber.Commands.UpdateBarber;
using BarberShop.Application.EntityCQ.Barber.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Queries;
using BarberShop.Application.Models.Dto.Balance;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Barber;
using BarberShop.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Balance.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly UriService _uriService;
        readonly IHttpContextAccessor httpContextAccessor;
        public BalanceService(IHttpContextAccessor httpContextAccessor, BarberShopDbContext dbContext, IMapper mapper, IWebHostEnvironment environment, UriService uriService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._environment = environment;
            this._uriService = uriService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Update(UpdateBalanceDto dto)
        {
            var balance = await _dbContext.Balances.Include(e => e.User).FirstOrDefaultAsync(e => e.UserId == dto.UserId && e.IsActive);
            if (balance == null)
                throw new NotFoundException(nameof(Balance), balance.Id);

            balance.UserBalance = dto.UserBalance;
            balance.UpdatedDate = DateTime.UtcNow.AddHours(4);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return balance.Id;
        }
    }
}
