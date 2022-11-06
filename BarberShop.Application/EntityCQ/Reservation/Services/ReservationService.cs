using AutoMapper;
using BarberShop.Application.EntityCQ.Reservation.Commands;
using BarberShop.Application.EntityCQ.Reservation.Interfaces;
using BarberShop.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Reservation.Services
{
    public class ReservationService: IReservationService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        public ReservationService(BarberShopDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<int> Create(CreateReservationCommand dto)
        {
            Domain.Reservation reservation = _mapper.Map<Domain.Reservation>(dto);

            reservation.ReservationStatusId = 1;

            await _dbContext.Reservations.AddAsync(reservation, CancellationToken.None);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return reservation.Id;
        }
    }
}
