using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Barber
{
    public class CreateBarberDto : IMapWith<CreateBarberCommand>
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBarberDto, CreateBarberCommand>();

        }
    }
}
