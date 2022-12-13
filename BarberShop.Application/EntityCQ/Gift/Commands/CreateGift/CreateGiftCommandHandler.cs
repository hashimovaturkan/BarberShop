using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Gift.Commands.CreateGift
{
    public class CreateGiftCommand : RequestTemplate, IMapWith<Domain.Gift>
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGiftCommand, Domain.Gift>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(employeeContactCreateDto => employeeContactCreateDto.UserIp));
        }
    }
}
