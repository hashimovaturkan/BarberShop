using AutoMapper;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.Common.Mappings;
using IntraNet.Application.Models.Vm.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.User
{
    public class UserListVm : IMapWith<Domain.User>
    {
        public int Id { get; set; }
        public int FilialId { get; set; }
        public string FilialName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BarberShop.Domain.User, UserListVm>()
                .ForMember(userLoginVm => userLoginVm.FilialName,
                    opt => opt.MapFrom(user => user.Filial.Name))
                .ForMember(userLoginVm => userLoginVm.ImageUrl,
                    opt => opt.MapFrom(user => user.ImageUrl.GetFile()));
        }
    }
}
