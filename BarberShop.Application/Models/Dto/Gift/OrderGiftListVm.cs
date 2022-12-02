using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.Models.Dto.Barber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Gift
{
    public class OrderGiftListVm : IMapWith<Domain.UserGiftRelation>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.UserGiftRelation, OrderGiftListVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(gift => gift.GiftId))
                .ForMember(vm => vm.Name, opt => opt.MapFrom(gift => gift.Gift.Name))
                .ForMember(vm => vm.Price, opt => opt.MapFrom(gift => gift.Gift.Price))
                .ForMember(vm => vm.Status, opt => opt.MapFrom(gift => gift.Status));

        }

    }
}
