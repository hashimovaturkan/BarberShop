using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Barber.Commands.UpdateBarber;
using BarberShop.Application.EntityCQ.Barber.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Queries;
using BarberShop.Application.Enums;
using BarberShop.Application.Models.Dto.Barber;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Barber;
using BarberShop.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.WebApi.Controllers
{
    public class BarberController : BaseController
    {
        private readonly IBarberService _barberService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IUserRoleService _userRoleService;

        public BarberController(IBarberService barberService, IConfiguration configuration, IMapper mapper, IWebHostEnvironment environment,
            IUserRoleService userRoleService)
        {
            _barberService = barberService;
            _environment = environment;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [HttpPost("BarberList")]
        public async Task<ActionResult<ResponseListTemplate<List<BarberListDto>>>> Get([FromQuery]GetBarberListQuery query)
        {
            if (query.PageNumber == 0)
                query.PageNumber = 1;
            if (query.PageSize == 0)
                query.PageSize = 10;

            var vm = await _barberService.GetList(query, Request.Path.Value);
            return Ok(vm);
        }

        [HttpPost("BarberSearchList")]
        public async Task<ActionResult<ResponseListTemplate<List<BarberListDto>>>> Get([FromQuery] GetBarberListQuery query, [FromBody] BarberDto? barberListDto)
        {
            if (query.PageNumber == 0)
                query.PageNumber = 1;
            if (query.PageSize == 0)
                query.PageSize = 10;

            var vm = await _barberService.GetSearchList(query, barberListDto.SearchingWord, Request.Path.Value);
            return Ok(vm);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateBarberDto createBarberDto)
        {
            var command = _mapper.Map<CreateBarberCommand>(createBarberDto);
            SetUserInfo();
            command.UserId = UserId;
            command.UserIp = UserIp;
            var id = await _barberService.Create(command);

            return Ok(id);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateBarberDto updateBarberDto)
        {
            var command = _mapper.Map<UpdateBarberCommand>(updateBarberDto);
            SetUserInfo();
            command.UserId = UserId;
            command.UserIp = UserIp;
            var id = await _barberService.Update(command);

            return Ok(id);
        }
    }
}
