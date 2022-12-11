using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.AdditinalService.Commands;
using BarberShop.Application.EntityCQ.AdditinalService.Interfaces;
using BarberShop.Application.EntityCQ.Service.Commands;
using BarberShop.Application.EntityCQ.Service.Interfaces;
using BarberShop.Application.EntityCQ.Service.Queries;
using BarberShop.Application.Models.Dto.AdditionalService;
using BarberShop.Application.Models.Dto.Service;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.AdditionalService;
using BarberShop.Application.Models.Vm.Service;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.WebApi.Controllers
{
    public class AdditionalServiceController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;
        private readonly IAdditionalServiceService _serviceService;

        public AdditionalServiceController(IUserService userService, IConfiguration configuration, IMapper mapper,
            IUserRoleService userRoleService, IAdditionalServiceService serviceService)
        {
            _userService = userService;
            _serviceService = serviceService;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateAdditionalServiceDto createServiceDto)
        {
            var command = _mapper.Map<CreateAdditionalServiceCommand>(createServiceDto);
            SetUserInfo();
            command.UserId = UserId;
            command.UserIp = UserIp;
            var id = await _serviceService.Create(command);

            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<AdditionalServiceLookupDto>>> Get()
        {
            var vm = await _serviceService.Get();
            return Ok(vm);
        }

    }
}
