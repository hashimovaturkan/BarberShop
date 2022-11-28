using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Barber.Commands.CreateBarber;
using BarberShop.Application.EntityCQ.Filial.Commands;
using BarberShop.Application.EntityCQ.Filial.Interfaces;
using BarberShop.Application.Enums;
using BarberShop.Application.Models.Dto.Barber;
using BarberShop.Application.Models.Dto.Filial;
using BarberShop.Application.Models.Dto.User;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.WebApi.Attributes;
using IntraNet.Application.Models.Vm.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.WebApi.Controllers
{
    public class FilialController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;
        private readonly IFilialService _filialService;

        public FilialController(IUserService userService, IConfiguration configuration, IMapper mapper,
            IUserRoleService userRoleService, IFilialService filialService)
        {
            _userService = userService;
            _filialService = filialService;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateFilialDto createFilialDto)
        {
            var command = _mapper.Map<CreateFilialCommand>(createFilialDto);
            SetUserInfo();
            command.UserId = UserId;
            command.UserIp = UserIp;
            var id = await _filialService.Create(command);

            return Ok(id);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<FilialListDto>>> Get()
        {
            var vm = await _filialService.GetList();
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<FilialDetailsVm>> Get([FromBody] FilialDetailsDto dto)
        {
            var filial = await _filialService.Get(dto.Id);

            return Ok(filial);
        }
    }
}
