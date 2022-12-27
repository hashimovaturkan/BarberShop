using BarberShop.Application.EntityCQ.Gift.Commands.CreateGift;
using BarberShop.Application.EntityCQ.Gift.Commands.DeleteGift;
using BarberShop.Application.EntityCQ.Gift.Commands.GiftOrder;
using BarberShop.Application.EntityCQ.Gift.Commands.UpdateGift;
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
        Task<bool> OrderGift(OrderGiftCommand command);
        Task<bool> DeleteOrderGift(OrderGiftCommand command);
        Task<List<OrderGiftListVm>> GetOrderGiftList(int userId);
        Task<int> Create(CreateGiftCommand dto);
        Task<int> Update(UpdateGiftCommand dto);
        Task<int> Delete(DeleteGiftCommand dto);
    }
}
