using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Admin.Commands
{
    public class CreateAdminCommand : RequestTemplate
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAdminCommand, Domain.Admin>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(employeeContactCreateDto => employeeContactCreateDto.UserIp));
        }
    }
}
