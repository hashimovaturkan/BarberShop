using AutoMapper;
using BarberShop.Application.EntitiesCQ.User.Commands.CreateUser;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.EntitiesCQ.User.Queries.UserLogin;
using BarberShop.Application.EntitiesCQ.UserRole.Interfaces;
using BarberShop.Application.EntityCQ.Admin.Commands;
using BarberShop.Application.EntityCQ.Admin.Interfaces;
using BarberShop.Application.EntityCQ.Admin.Services;
using BarberShop.Application.EntityCQ.User.Commands.UpdateUser;
using BarberShop.Application.EntityCQ.User.Queries.UserList;
using BarberShop.Application.Models.Dto.Admin;
using BarberShop.Application.Models.Dto.Mail;
using BarberShop.Application.Models.Dto.User;
using BarberShop.Application.Models.Vm.Admin;
using BarberShop.Application.Models.Vm.Roles;
using BarberShop.Application.Models.Vm.User;
using IntraNet.Application.EntitiesCQ.User.Services;
using IntraNet.Application.Models.Vm.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberShop.WebApi.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IUserRoleService _userRoleService;

        public AdminController(IAdminService adminService, IConfiguration configuration, IMapper mapper, IWebHostEnvironment environment,
            IUserRoleService userRoleService)
        {
            _adminService = adminService;
            _environment = environment;
            (_configuration, _mapper) = (configuration, mapper);
            _userRoleService = userRoleService;
        }

        [AllowAnonymous]
        [HttpGet("/api/Roles")]
        public async Task<ActionResult<List<RoleVm>>> Roles ()
        {
            var roles = await _adminService.GetRoles();

            return Ok(roles);
        }

        [AllowAnonymous]
        [HttpPost("/api/Admin/Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            UserLoginQuery query = _mapper.Map<UserLoginQuery>(loginDto);
            query.LangId = LangId;
            query.UserIp = UserIp;

            var vm = await _adminService.LoginAsync(query);
            if (vm is null) return Unauthorized();

            List<Claim> authClaims = new List<Claim>
            {
                new("UserId", vm.UserId.ToString()),
                new("LangId", vm.LangId.ToString()),
                new("Email", vm.Email),
                new("FullName", vm.FullName),
                new("Role", vm.Role.ToString()),
                new("Phone", vm.Phone),
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
                    new CookieOptions { Expires = DateTime.UtcNow.ToLocalTime().AddDays(1) });


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo.ToLocalTime().ToString()
            });
        }


        [AllowAnonymous]
        [HttpPost("/api/Admin/Registration")]
        public async Task<ActionResult<int>> Registration([FromBody] CreateAdminDto createUserDto)
        {
            var command = _mapper.Map<CreateAdminCommand>(createUserDto);
            command.UserIp = UserIp;
            command.UserId = UserId;
            var userId = await _adminService.CreateAsync(command);

            return Ok(userId);
        }


        [HttpGet]
        public async Task<ActionResult<AdminDetailsVm>> AdminDetails()
        {
            var user = await _adminService.Get(UserId);

            return Ok(user);
        }


        [HttpPost("/api/Admin/Update")]
        public async Task<IActionResult> Update([FromBody] UpdateAdminDto updateUserDto)
        {
            var command = _mapper.Map<UpdateAdminCommand>(updateUserDto);
            command.UserId = UserId;
            command.UserIp = UserIp;
            await _adminService.Update(command);


            return Ok();


        }



    }
}
