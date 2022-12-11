using BarberShop.Application.EntityCQ.Filial.Commands;
using BarberShop.Application.EntityCQ.Service.Commands;
using BarberShop.Application.EntityCQ.Service.Queries;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Service.Interfaces
{
    public interface IServiceService : IBaseService
    {
        Task<ResponseListTemplate<List<ServiceLookupDto>>> GetList(GetServiceListQuery query, string route);
        Task<List<ServiceLookupDto>> Get();
        Task<int> Create(CreateServiceCommand dto);
    }
}
