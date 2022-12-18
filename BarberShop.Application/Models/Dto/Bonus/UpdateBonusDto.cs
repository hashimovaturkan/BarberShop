using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Dto.Balance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Bonus
{
    public class UpdateBonusDto : IMapWith<Domain.User>
    {
        public int? UserId { get; set; }
        public int? UserBonuses { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBonusDto, Domain.User>();
        }
    }
}
