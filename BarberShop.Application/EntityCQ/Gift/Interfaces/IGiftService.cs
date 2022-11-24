using BarberShop.Application.Models.Dto.Gift;
using BarberShop.Application.Models.Vm.Filial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Gift.Interfaces
{
    public interface IGiftService : IBaseService
    {
        Task<List<GiftListDto>> GetList();
    }
}
