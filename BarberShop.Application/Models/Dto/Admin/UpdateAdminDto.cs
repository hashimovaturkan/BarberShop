using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Admin.Commands;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Admin
{
    public class UpdateAdminDto : IMapWith<UpdateAdminCommand>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateAdminDto, UpdateAdminCommand>();
        }
    }
}
