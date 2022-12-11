using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Filial.Commands;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Service.Commands
{
    public class CreateServiceCommand : RequestTemplate, IMapWith<Domain.Service>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateServiceCommand, Domain.Service>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(employeeContactCreateDto => employeeContactCreateDto.UserIp));
        }
    }
}
