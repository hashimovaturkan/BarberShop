using AutoMapper;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.User.Queries.UserLogin;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.EntityCQ.User.Queries.UserList;
using BarberShop.Application.Enums;
using BarberShop.Application.Models.Dto.User;
using BarberShop.Application.Models.Template;
using BarberShop.Application.Models.Vm.User;
using BarberShop.WebApi.Attributes;
using IntraNet.Application.Models.Vm.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QRCoder;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberShop.WebApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IUserRoleService _userRoleService;

        public UserController(IUserService userService, IConfiguration configuration, IMapper mapper, IWebHostEnvironment environment,
            IUserRoleService userRoleService)
        {
            _userService = userService;
            _environment = environment;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [AllowAnonymous]
        [HttpPost("/api/Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            UserLoginQuery query = _mapper.Map<UserLoginQuery>(loginDto);
            query.LangId = LangId;
            query.UserIp = UserIp;

            UserLoginVm vm = await _userService.LoginAsync(query);
            if (vm is null) return Unauthorized();

            List<Claim> authClaims = new List<Claim>
            {
                new("UserId", vm.UserId.ToString()),
                new("LangId", vm.LangId.ToString()),
                new("Email", vm.Email),
                new("FullName", vm.FullName),
                new("Phone", vm.Phone),
                new("FilialId", vm.FilialId.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey authSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.ToLocalTime().AddYears(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            if (string.IsNullOrWhiteSpace(Response.Headers["LangId"]))
                Response.Cookies.Append("LangId", "1",
                    new CookieOptions {Expires = DateTime.UtcNow.ToLocalTime().AddDays(1)});


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo.ToLocalTime().ToString()
            });
        }


        [AllowAnonymous]
        [HttpPost("/api/Registration")]
        public async Task<ActionResult<int>> Registration([FromBody] CreateUserDto createUserDto)
        {
            var command = _mapper.Map<CreateUserCommand>(createUserDto);
            command.UserIp = UserIp;
            command.UserId = UserId;
            var userId = await _userService.CreateAsync(command);

            await _userRoleService.AddRoleToUser("User", userId, UserIp);

            return Ok(userId);
        }

        [AllowAnonymous]
        [HttpPost("/api/SendSms")]
        public async Task<ActionResult<bool>> SendSms([FromBody] SendSmsDto sendSmsDto)
        {
            var isSucces = await _userService.SendSms(sendSmsDto.PhoneNumber);

            if (isSucces)
                return Ok();
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("/api/SmsVerification")]
        public async Task<ActionResult<bool>> SmsVerification([FromBody] SmsVerificationDto sendSmsDto)
        {
            var isSucces = await _userService.VerifyPhoneNumber(sendSmsDto.PhoneNumber, sendSmsDto.Value);

            if (isSucces)
                return Ok();
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("/api/ResetPassword")]
        public async Task<ActionResult<bool>> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var userId = await _userService.ResetPassword(resetPasswordDto.PhoneNumber, resetPasswordDto.Password);

            return Ok(userId);
        }


        [HttpPost("/api/UserList")]
        public async Task<ActionResult<UserLookUpDto>> GetAll([FromBody] GetUserListDto request)
        {
            if (request.Number == 0)
                request.Number = 1;
            if (request.Size == 0)
                request.Size = 20;

            var query = _mapper.Map<GetUserListQuery>(request);
            var vm = await _userService.GetList(query);

            return Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<UserDetailsVm>> UserDetails()
        {
            var user = await _userService.Get(UserId);

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateUserDto updateUserDto)
        {
            var command = _mapper.Map<UpdateUserCommand>(updateUserDto);
            command.UserId = UserId;
            command.UserIp = UserIp;
            await _userService.Update(command);


            return NoContent();


        }

        [HttpPost]
        public async Task<ActionResult<UserDetailsVm>> UserDetails([FromBody] UserDetailsDto dto)
        {
            var user = await _userService.Get(dto.Id);

            return Ok(user);
        }


    }
}