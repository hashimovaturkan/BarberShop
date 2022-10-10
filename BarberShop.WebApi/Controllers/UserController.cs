using AutoMapper;
using BarberShop.Application.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BarberShop.Application.Enums;
using BarberShop.WebApi.Attributes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.Models.Vm.User;
using BarberShop.Application.Models.Dto.User;
using BarberShop.Application.EntitiesCQ.User.Queries.UserLogin;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntitiesCQ.User.Queries.PassResetGetUser;
using BarberShop.Application.Models.Template;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using BarberShop.Application.Common.Components;

namespace BarberShop.WebApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;

        public UserController(IUserService userService, IConfiguration configuration, IMapper mapper,
            IUserRoleService userRoleService)
        {
            _userService = userService;
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
                new("Name", vm.Name),
                new("Surname", vm.Surname),
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
        public async Task<ActionResult<bool>> SendSms(string phoneNumber)
        {
            var isSucces = await _userService.SendSms(phoneNumber);

            if (isSucces)
                return Ok();
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("/api/SmsVerification")]
        public async Task<ActionResult<bool>> SmsVerification(string phoneNumber, string value)
        {
            var isSucces = await _userService.VerifyPhoneNumber(phoneNumber, value);

            if (isSucces)
                return Ok();
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("/api/ResetPassword")]
        public async Task<ActionResult<bool>> ResetPassword(string phoneNumber, string password)
        {
            var userId = await _userService.ResetPassword(phoneNumber, password);

            return Ok(userId);
        }


    }
}