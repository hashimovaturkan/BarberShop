using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Barber.Commands.UpdateBarber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Barber
{
    public class UpdateBarberDto : IMapWith<UpdateBarberCommand>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int? FilialId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBarberDto, UpdateBarberCommand>();

        }
    }
}
