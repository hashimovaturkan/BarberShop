using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Gift.Commands.CreateGift;
using BarberShop.Application.EntityCQ.Gift.Commands.DeleteGift;
using BarberShop.Application.EntityCQ.Gift.Commands.GiftOrder;
using BarberShop.Application.EntityCQ.Gift.Commands.UpdateGift;
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

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateGiftDto createGiftDto)
        {
            var command = _mapper.Map<CreateGiftCommand>(createGiftDto);
            SetUserInfo();
            command.UserId = UserId;
            command.UserIp = UserIp;
            var id = await _giftService.Create(command);

            return Ok(id);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateGiftDto updateGiftDto)
        {
            var command = _mapper.Map<UpdateGiftCommand>(updateGiftDto);
            SetUserInfo();
            command.UserId = UserId;
            command.UserIp = UserIp;
            var id = await _giftService.Update(command);

            return Ok(id);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteGiftDto deleteGiftDto)
        {
            var command = _mapper.Map<DeleteGiftCommand>(deleteGiftDto);
            var id = await _giftService.Delete(command);

            return Ok();
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
