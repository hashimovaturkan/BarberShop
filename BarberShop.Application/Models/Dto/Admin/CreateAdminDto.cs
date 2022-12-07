using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntityCQ.Admin.Commands;
using BarberShop.Application.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Admin
{
    public class CreateAdminDto : IMapWith<CreateAdminCommand>
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAdminDto, CreateAdminCommand>()
                .ForMember(createUserCommand => createUserCommand.Email, opt => opt.MapFrom(createUserDto => createUserDto.Email.Trim().ToLower()));
        }
    }
}
