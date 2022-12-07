using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Vm.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.Admin
{
    public class AdminLoginVm : IMapWith<Domain.Admin>
    {
        public int UserId { get; set; }
        public int LangId { get; set; } = 1;
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Admin, AdminLoginVm>()
                .ForMember(userLoginVm => userLoginVm.UserId,
                    opt => opt.MapFrom(user => user.Id));
        }
    }
}
