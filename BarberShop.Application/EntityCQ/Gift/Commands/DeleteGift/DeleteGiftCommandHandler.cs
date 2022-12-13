using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Gift.Commands.DeleteGift
{
    public class DeleteGiftCommand : RequestTemplate, IMapWith<Domain.Gift>
    {
        public int Id { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteGiftCommand, Domain.Gift>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(employeeContactCreateDto => employeeContactCreateDto.UserIp));
        }
    }
}
