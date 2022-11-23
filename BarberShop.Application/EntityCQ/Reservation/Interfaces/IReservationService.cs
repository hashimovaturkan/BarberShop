using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Reservation.Commands;
using BarberShop.Application.EntityCQ.Reservation.Queries;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Reservation.Interfaces
{
    public interface IReservationService : IBaseService
    {
        Task<int> Create(CreateReservationCommand dto);
        Task<ResponseListTemplate<List<ReservationListDto>>> GetList(GetReservationListQuery query,int id, string route);
    }
}
