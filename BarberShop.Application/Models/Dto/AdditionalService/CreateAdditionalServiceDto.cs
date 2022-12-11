using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.AdditinalService.Commands;
using BarberShop.Application.EntityCQ.Service.Commands;
using BarberShop.Application.Models.Dto.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.AdditionalService
{
    public class CreateAdditionalServiceDto : IMapWith<CreateAdditionalServiceCommand>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAdditionalServiceDto, CreateAdditionalServiceCommand>();

        }
    }
}
