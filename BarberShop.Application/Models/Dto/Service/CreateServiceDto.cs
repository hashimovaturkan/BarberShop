using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Filial.Commands;
using BarberShop.Application.EntityCQ.Service.Commands;
using BarberShop.Application.Models.Dto.Filial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Service
{
    public class CreateServiceDto : IMapWith<CreateServiceCommand>
    {
        public string Name { get; set; }
        public double? Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateServiceDto, CreateServiceCommand>();

        }
    }
}
