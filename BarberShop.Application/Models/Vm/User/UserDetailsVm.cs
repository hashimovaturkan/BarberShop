using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Vm.User;
using BarberShop.Domain;
using System.Collections.Generic;

namespace IntraNet.Application.Models.Vm.User
{
    public class UserDetailsVm : IMapWith<BarberShop.Domain.User>
    {
        public int Id { get; set; }
        public int FilialId { get; set; }
        public string FilialName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BarberShop.Domain.User, UserDetailsVm>()
                .ForMember(userLoginVm => userLoginVm.Id,
                    opt => opt.MapFrom(user => user.Id))
                .ForMember(userLoginVm => userLoginVm.FilialName,
                    opt => opt.MapFrom(user => user.Filial.Name));
        }
    }
}
