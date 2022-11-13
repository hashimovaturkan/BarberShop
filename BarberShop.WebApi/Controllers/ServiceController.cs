using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Reservation.Interfaces;
using BarberShop.Application.EntityCQ.Service.Interfaces;
using BarberShop.Application.EntityCQ.Service.Queries;
using BarberShop.Application.Enums;
using BarberShop.Application.Models.Dto.Service;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Application.Models.Vm.Service;
using BarberShop.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Sentry;

namespace BarberShop.WebApi.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;
        private readonly IServiceService _serviceService;

        public ServiceController(IUserService userService, IConfiguration configuration, IMapper mapper,
            IUserRoleService userRoleService, IServiceService serviceService)
        {
            _userService = userService;
            _serviceService = serviceService;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseListTemplate<List<ServiceLookupDto>>>> Get([FromBody] GetServiceListDto dto)
        {
            var query = _mapper.Map<GetServiceListQuery>(dto);

            if (query.Number == 0)
                query.Number = 1;
            if (query.Size == 0)
                query.Size = 20;

            query.UserId = UserId;
            query.UserIp = UserIp;
            query.Route = Request.Path.Value;

            var vm = await _serviceService.GetList(query);
            return Ok(vm);
        }


        [HttpGet]
        public async Task<ActionResult<List<ServiceLookupDto>>> Get()
        {
            var vm = await _serviceService.Get();
            return Ok(vm);
        }

    }
}
