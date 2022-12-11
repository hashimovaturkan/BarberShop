using AutoMapper;
using BarberShop.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.Reservation
{
    public class ReservationListDto : IMapWith<Domain.Reservation>
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int? FirstServiceId { get; set; }
        public string? FirstServiceName { get; set; }
        public int? SecondServiceId { get; set; }
        public string? SecondServiceName { get; set; }
        public int FilialId { get; set; }
        public string FilialName { get; set; }
        public int ReservationStatusId { get; set; }
        public string ReservationStatusName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Reservation, ReservationListDto>()
                .ForMember(x => x.ReservationStatusName,
                    z => z.MapFrom(y => y.ReservationStatus.Name))
                .ForMember(x => x.FilialName,
                    z => z.MapFrom(y => y.Filial.Name))
                .ForMember(x => x.FirstServiceName,
                    z => z.MapFrom(y => y.FirstService.Name))
                .ForMember(x => x.SecondServiceName,
                    z => z.MapFrom(y => y.SecondService.Name));
        }
    }
}
