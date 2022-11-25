using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Reservation.Commands
{
    public class CreateReservationCommand : RequestTemplate, IMapWith<Domain.Reservation>
    {
        public string ReservationDate { get; set; }
        public int? FirstServiceId { get; set; }
        public int? SecondServiceId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateReservationCommand, Domain.Reservation>()
                .ForMember(emp => emp.CreatedIp,
                    opt => opt.MapFrom(dto => dto.UserIp));
        }
    }
}
