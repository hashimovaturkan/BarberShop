using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Template;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.User.Commands.UpdateUser
{
    public class UpdateUserCommand : RequestTemplate, IMapWith<Domain.User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int FilialId { get; set; }
        public string Email { get; set; }
        public IFormFile? Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserCommand, Domain.User>();
        }
    }
}
