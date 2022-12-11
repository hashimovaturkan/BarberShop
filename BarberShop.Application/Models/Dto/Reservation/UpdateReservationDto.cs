using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Reservation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Reservation
{
    public class UpdateReservationDto 
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int ReservationStatusId { get; set; }
    }
}
