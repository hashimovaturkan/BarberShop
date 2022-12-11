using BarberShop.Application.EntityCQ.Reservation.Commands;
using BarberShop.Application.EntityCQ.Reservation.Queries;
using BarberShop.Application.Models.Dto.Reservation;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Application.Models.Vm.Reservation;
using BarberShop.Application.Models.Vm.ReservationStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.ReservationStatus.Interfaces
{
    public interface IReservationStatusService : IBaseService
    {
        Task<List<ReservationStatusListVm>> GetList();
    }
}
