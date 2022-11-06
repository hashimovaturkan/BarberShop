using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Filial.Interfaces;
using BarberShop.Application.EntityCQ.Reservation.Commands;
using BarberShop.Application.EntityCQ.Reservation.Interfaces;
using BarberShop.Application.Models.Dto.Barber;
using BarberShop.Application.Models.Dto.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.WebApi.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;
        private readonly IReservationService _reservationService;

        public ReservationController(IUserService userService, IConfiguration configuration, IMapper mapper,
            IUserRoleService userRoleService, IReservationService reservationService)
        {
            _userService = userService;
            _reservationService = reservationService;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateReservationDto createReservationDto)
        {
            var command = _mapper.Map<CreateReservationCommand>(createReservationDto);
            SetUserInfo();
            command.UserId = UserId;
            command.UserIp = UserIp;
            var id = await _reservationService.Create(command);

            return Ok(id);
        }
    }
}
