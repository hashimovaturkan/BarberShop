using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Barber.Commands.UpdateBarber
{
    public class UpdateBarberCommand : RequestTemplate, IMapWith<Domain.Barber>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FilialId { get; set; }
        public string? Image { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBarberCommand, Domain.Barber>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(employeeContactCreateDto => employeeContactCreateDto.UserIp));
        }
    }
}
