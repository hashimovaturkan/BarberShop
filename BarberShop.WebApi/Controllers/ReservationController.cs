using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Filial.Interfaces;
using BarberShop.Application.EntityCQ.Filial.Services;
using BarberShop.Application.EntityCQ.Reservation.Commands;
using BarberShop.Application.EntityCQ.Reservation.Interfaces;
using BarberShop.Application.EntityCQ.Reservation.Queries;
using BarberShop.Application.EntityCQ.ReservationStatus.Interfaces;
using BarberShop.Application.Models.Dto.Barber;
using BarberShop.Application.Models.Dto.Reservation;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Application.Models.Vm.Reservation;
using BarberShop.Application.Models.Vm.ReservationStatus;
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
        private readonly IReservationStatusService _reservationStatusService;

        public ReservationController(IUserService userService, IConfiguration configuration, IMapper mapper,
            IUserRoleService userRoleService, IReservationService reservationService, IReservationStatusService reservationStatusService)
        {
            _userService = userService;
            _reservationService = reservationService;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
            _reservationStatusService = reservationStatusService;
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

        [HttpPost("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateReservationDto updateReservationDto)
        {
            var id = await _reservationService.Update(updateReservationDto);

            return Ok(id);
        }

        [HttpPost("ReservationList")]
        public async Task<ActionResult<ResponseListTemplate<List<ReservationListDto>>>> Get([FromQuery]GetReservationListQuery query)
        {
            if (query.PageNumber == 0)
                query.PageNumber = 1;
            if (query.PageSize == 0)
                query.PageSize = 10;

            var vm = await _reservationService.GetList(query,UserId, Request.Path.Value);
            return Ok(vm);
        }

        [HttpPost("AllReservationList")]
        public async Task<ActionResult<ResponseListTemplate<List<ReservationListDto>>>> GetAll([FromQuery] GetReservationListQuery query)
        {
            if (query.PageNumber == 0)
                query.PageNumber = 1;
            if (query.PageSize == 0)
                query.PageSize = 10;

            var vm = await _reservationService.GetAllList(query, UserId, Request.Path.Value);
            return Ok(vm);
        }

        [HttpGet("ReservationStatusList")]
        public async Task<ActionResult<List<ReservationStatusListVm>>> Get()
        {
            var vm = await _reservationStatusService.GetList();
            return Ok(vm);
        }
    }
}
