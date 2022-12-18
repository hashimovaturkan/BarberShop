using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.Models.Dto.User;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Balance
{
    public class UpdateBalanceDto : IMapWith<Domain.Balance>
    {
        public int? UserId { get; set; }
        public double UserBalance { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBalanceDto, Domain.Balance>();
        }
    }
}
