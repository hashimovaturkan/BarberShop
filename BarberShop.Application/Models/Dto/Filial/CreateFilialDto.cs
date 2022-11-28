using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Filial.Commands;
using BarberShop.Application.Models.Dto.Barber;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Filial
{
    public class CreateFilialDto : IMapWith<CreateFilialCommand>
    {
        public string Name { get; set; }
        public string Lang { get; set; }
        public string Long { get; set; }
        public string? Address { get; set; }
        public string? OpenTime { get; set; }
        public string? EndTime { get; set; }
        public string? Description { get; set; }
        //public IFormFile? Image { get; set; }
        public string? Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFilialDto, CreateFilialCommand>();

        }
    }
}
