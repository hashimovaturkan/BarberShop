using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Gift.Commands.DeleteGift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Gift
{
    public class DeleteGiftDto : IMapWith<DeleteGiftCommand>
    {
        public int Id { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteGiftDto, DeleteGiftCommand>();

        }
    }
}
