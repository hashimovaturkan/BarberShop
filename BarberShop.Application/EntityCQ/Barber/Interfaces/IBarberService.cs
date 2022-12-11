using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Barber.Commands.UpdateBarber;
using BarberShop.Application.EntityCQ.Barber.Queries;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Barber;
using BarberShop.Application.Models.Vm.Filial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Barber.Interfaces
{
    public interface IBarberService : IBaseService
    {
        Task<ResponseListTemplate<List<BarberListDto>>> GetList(GetBarberListQuery query, string route);
        Task<int> Create(CreateBarberCommand dto);
        Task<int> Update(UpdateBarberCommand dto);
    }
}
