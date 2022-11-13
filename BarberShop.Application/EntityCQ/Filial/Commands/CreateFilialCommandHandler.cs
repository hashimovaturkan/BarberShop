using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.Models.Template;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Filial.Commands
{
    public class CreateFilialCommand : RequestTemplate, IMapWith<Domain.Filial>
    {
        public string Name { get; set; }
        public string Lang { get; set; }
        public string Long { get; set; }
        public string? Address { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFilialCommand, Domain.Filial>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(employeeContactCreateDto => employeeContactCreateDto.UserIp));
        }
    }
}
