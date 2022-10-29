using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Filial.Interfaces;
using BarberShop.Application.Enums;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.WebApi.Attributes;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<FilialListDto>>> Get()
        {
            var vm = await _filialService.GetList();
            return Ok(vm);
        }
    }
}
