using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Gift.Commands.UpdateGift
{
    public class UpdateGiftCommand : RequestTemplate, IMapWith<Domain.Gift>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGiftCommand, Domain.Gift>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(employeeContactCreateDto => employeeContactCreateDto.UserIp));
        }
    }
}
