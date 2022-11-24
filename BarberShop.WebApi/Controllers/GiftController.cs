﻿using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Filial.Commands;
using BarberShop.Application.EntityCQ.Filial.Interfaces;
using BarberShop.Application.EntityCQ.Gift.Interfaces;
using BarberShop.Application.Models.Dto.Filial;
using BarberShop.Application.Models.Dto.Gift;
using BarberShop.Application.Models.Vm.Filial;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.WebApi.Controllers
{
    public class GiftController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;
        private readonly IGiftService _giftService;

        public GiftController(IUserService userService, IConfiguration configuration, IMapper mapper,
            IUserRoleService userRoleService, IGiftService giftService)
        {
            _userService = userService;
            _giftService = giftService;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<GiftListDto>>> Get()
        {
            var vm = await _giftService.GetList();
            return Ok(vm);
        }
    }

}