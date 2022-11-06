using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Reservation.Commands;
using BarberShop.Application.Models.Dto.Barber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Reservation
{
    public class CreateReservationDto : IMapWith<CreateReservationCommand>
    {
        public DateTime ReservationDate { get; set; }
        public int? FirstServiceId { get; set; }
        public int? SecondServiceId { get; set; }
        public int FilialId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateReservationDto, CreateReservationCommand>();

        }
    }
}
