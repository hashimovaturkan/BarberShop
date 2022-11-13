using BarberShop.Application.Models.Vm.AdditionalService;
using BarberShop.Application.Models.Vm.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.AdditinalService.Interfaces
{
    public interface IAdditionalServiceService : IBaseService
    {
        Task<List<AdditionalServiceLookupDto>> Get();
    }
}
