using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Barber.Commands.UpdateBarber;
using BarberShop.Application.EntityCQ.Barber.Queries;
using BarberShop.Application.Models.Dto.Balance;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Barber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Balance.Interfaces
{
    public interface IBalanceService : IBaseService
    {
        Task<int> Update(UpdateBalanceDto dto);
    }
}
