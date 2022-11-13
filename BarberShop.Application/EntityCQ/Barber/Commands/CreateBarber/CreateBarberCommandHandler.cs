using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.Models.Template;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber
{
    public class CreateBarberCommand : RequestTemplate, IMapWith<Domain.Barber>
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBarberCommand, Domain.Barber>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(employeeContactCreateDto => employeeContactCreateDto.UserIp));
        }
    }
}
