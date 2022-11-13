using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Filial.Commands;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Filial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Filial.Interfaces
{
    public interface IFilialService : IBaseService
    {
        Task<int> Create(CreateFilialCommand dto);
        Task<List<FilialListDto>> GetList();
        Task<FilialDetailsVm> Get(int Id);
    }
}
