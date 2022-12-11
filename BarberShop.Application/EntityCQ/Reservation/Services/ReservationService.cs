using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Exceptions;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.Common.Services;
using BarberShop.Application.EntityCQ.Reservation.Commands;
using BarberShop.Application.EntityCQ.Reservation.Interfaces;
using BarberShop.Application.EntityCQ.Reservation.Queries;
using BarberShop.Application.Models.Dto.Reservation;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Reservation;
using BarberShop.Persistence;
using Microsoft.EntityFrameworkCore;
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
        private readonly UriService _uriService;
        public ReservationService(BarberShopDbContext dbContext, IMapper mapper, UriService _uriService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._uriService = _uriService;
        }

        public async Task<int> Create(CreateReservationCommand dto)
        {
            Domain.Reservation reservation = _mapper.Map<Domain.Reservation>(dto);

            if (reservation.SecondServiceId == 0)
                reservation.SecondServiceId = null;

            reservation.ReservationStatusId = 1;
            reservation.FilialId =(int) _dbContext.Users.FirstOrDefaultAsync(e => e.Id == dto.UserId).Result.FilialId;

            await _dbContext.Reservations.AddAsync(reservation, CancellationToken.None);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return reservation.Id;
        }

        public async Task<ResponseListTemplate<List<ReservationListDto>>> GetAllList(GetReservationListQuery query, int id, string route)
        {
            var reservations = _dbContext.Reservations
                .Include(e => e.ReservationStatus)
                .Include(e => e.Filial)
                .Include(e => e.SecondService)
                .Include(e => e.FirstService);

            PaginationFilter paginationFilter = new PaginationFilter(query.PageNumber, query.PageSize);
            IQueryable<Domain.Reservation> surveyPagedQuery = paginationFilter.GetPagedList(reservations);

            int totalRecords = await reservations.CountAsync();

            List<Domain.Reservation> surveyPaged = await surveyPagedQuery.OrderBy(e => e.CreatedDate).ToListAsync();

            var surveyLookupDtoList = _mapper.Map<List<ReservationListDto>>(surveyPaged);

            ResponseListTemplate<List<ReservationListDto>> result = surveyLookupDtoList.CreatePagedReponse(paginationFilter, totalRecords, _uriService, route);

            return result;
        }

        public async Task<ResponseListTemplate<List<ReservationListDto>>> GetList(GetReservationListQuery query,int id, string route)
        {
            var reservations = _dbContext.Reservations
                .Include(e => e.ReservationStatus)
                .Include(e => e.Filial)
                .Include(e => e.SecondService)
                .Include(e => e.FirstService)
                .Where(e => e.IsActive && e.UserId == id);

            PaginationFilter paginationFilter = new PaginationFilter(query.PageNumber, query.PageSize);
            IQueryable<Domain.Reservation> surveyPagedQuery = paginationFilter.GetPagedList(reservations);

            int totalRecords = await reservations.CountAsync();

            List<Domain.Reservation> surveyPaged = await surveyPagedQuery.OrderBy(e => e.CreatedDate).ToListAsync();

            var surveyLookupDtoList = _mapper.Map<List<ReservationListDto>>(surveyPaged);
            
            ResponseListTemplate<List<ReservationListDto>> result = surveyLookupDtoList.CreatePagedReponse(paginationFilter, totalRecords, _uriService, route);

            return result;
            
        }

        public async Task<int> Update(UpdateReservationDto dto)
        {
            var reservation = await _dbContext.Reservations.FirstOrDefaultAsync(e => e.Id == dto.Id && e.IsActive);

            if (reservation == null)
                throw new NotFoundException(nameof(Reservation), reservation.Id);

            reservation.UpdatedDate = DateTime.UtcNow.AddHours(4);

            reservation.ReservationStatusId = dto.ReservationStatusId;
            reservation.ReservationDate = dto.ReservationDate;

            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return reservation.Id;
        }
    }
}
