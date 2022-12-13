using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Gift.Commands.UpdateGift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Gift
{
    public class UpdateGiftDto : IMapWith<UpdateGiftCommand>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGiftDto, UpdateGiftCommand>();

        }
    }
}
