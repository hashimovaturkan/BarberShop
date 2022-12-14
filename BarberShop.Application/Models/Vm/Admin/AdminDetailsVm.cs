using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Enums;
using IntraNet.Application.Models.Vm.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.Admin
{
    public class AdminDetailsVm : IMapWith<BarberShop.Domain.Admin>
    {
        public int Id { get; set; }
        public int Role { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string? ImageUrl { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<BarberShop.Domain.Admin, AdminDetailsVm>()
                .ForMember(userLoginVm => userLoginVm.RoleName,
                    opt => opt.MapFrom(user => (Enums.Roles)user.Role));
        }
    }
}
