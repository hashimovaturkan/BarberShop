using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Balance.Interfaces;
using BarberShop.Application.EntityCQ.Filial.Commands;
using BarberShop.Application.EntityCQ.Filial.Interfaces;
using BarberShop.Application.Models.Dto.Balance;
using BarberShop.Application.Models.Dto.Filial;
using BarberShop.Application.Models.Vm.Filial;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BarberShop.WebApi.Controllers
{
    public class BalanceController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;
        private readonly IBalanceService _balanceService;

        public BalanceController(IUserService userService, IConfiguration configuration, IMapper mapper,
            IUserRoleService userRoleService, IBalanceService balanceService)
        {
            _userService = userService;
            _balanceService = balanceService;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [HttpPost("Update")]
        public async Task<ActionResult<int>> Create([FromBody] UpdateBalanceDto dto)
        {
            if (!dto.UserId.HasValue || dto.UserId == 0)
                dto.UserId = UserId;

            var id = await _balanceService.Update(dto);
            return Ok();
        }
    }
}
