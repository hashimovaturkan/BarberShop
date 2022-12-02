using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Gift.Commands.GiftOrder
{
    public class OrderGiftCommand : RequestTemplate
    {
        public int GiftId { get; set; }

    }
}
