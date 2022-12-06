using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Gift.Commands.GiftOrder;
using BarberShop.Application.EntityCQ.Gift.Interfaces;
using BarberShop.Application.Models.Dto.Gift;
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

        [HttpGet]
        public async Task<ActionResult<List<GiftListDto>>> Get()
        {
            var vm = await _giftService.GetList();
            return Ok(vm);
        }

        [HttpGet("OrderGiftList")]
        public async Task<ActionResult<List<OrderGiftListVm>>> GetOrderGiftList()
        {
            var vm = await _giftService.GetOrderGiftList(UserId);
            return Ok(vm);
        }

        [HttpPost("OrderGift")]
        public async Task<ActionResult> OrderGift([FromBody] GiftOrderDto giftOrderDto)
        {
            var command = _mapper.Map<OrderGiftCommand>(giftOrderDto);
            command.UserId = UserId;
            command.UserIp = UserIp;
            await _giftService.OrderGift(command);

            return Ok();
        }
    }

}
