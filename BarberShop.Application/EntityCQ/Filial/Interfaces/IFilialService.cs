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
        Task<List<FilialListDto>> GetList();
    }
}
