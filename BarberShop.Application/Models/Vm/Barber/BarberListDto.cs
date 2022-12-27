using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Vm.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.Barber
{
    public class BarberListDto : IMapWith<Domain.Barber>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? FilialId { get; set; }
        public string? FilialName { get; set; }
        public int? PhotoId { get; set; }
        public string? Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Barber, BarberListDto>()
                .ForMember(vm => vm.FilialName,
                    opt => opt.MapFrom(u => u.Filial.Name));
        }
    }
}
