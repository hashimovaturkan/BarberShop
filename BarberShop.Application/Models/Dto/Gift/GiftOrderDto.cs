using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Gift.Commands.GiftOrder;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Gift
{
    public class GiftOrderDto : IMapWith<OrderGiftCommand>
    {
        public int GiftId { get; set; }
        public int? UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GiftOrderDto, OrderGiftCommand>();
        }
    }
}
