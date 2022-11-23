using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Filial.Interfaces;
using BarberShop.Application.EntityCQ.Reservation.Commands;
using BarberShop.Application.EntityCQ.Reservation.Interfaces;
using BarberShop.Application.EntityCQ.Reservation.Queries;
using BarberShop.Application.Models.Dto.Barber;
using BarberShop.Application.Models.Dto.Reservation;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Reservation;
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

        [HttpPost("ReservationList")]
        public async Task<ActionResult<ResponseListTemplate<List<ReservationListDto>>>> Get(GetReservationListQuery query)
        {
            if (query.Number == 0)
                query.Number = 1;
            if (query.Size == 0)
                query.Size = 20;

            var vm = await _reservationService.GetList(query,UserId, Request.Path.Value);
            return Ok(vm);
        }
    }
}
