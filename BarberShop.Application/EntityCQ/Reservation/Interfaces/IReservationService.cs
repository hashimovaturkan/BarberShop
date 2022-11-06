using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Reservation.Commands;
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
    }
}
