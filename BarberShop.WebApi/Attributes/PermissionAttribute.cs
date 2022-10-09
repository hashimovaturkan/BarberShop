using BarberShop.Application.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BarberShop.Application.Common.Exceptions;
using BarberShop.Application.EntitiesCQ.User.Interfaces;
using BarberShop.Application.Models.Vm.User;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : ActionFilterAttribute
    {
        private UserClaims _claim;
        private IUserService _userService;
        private IMemoryCache _cache;

        public PermissionAttribute(UserClaims claim)
        {
            _claim = claim;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _userService = context.HttpContext.RequestServices.GetService<IUserService>();
            _cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
            
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                throw new UnauthorizedException("Not authorized", "Userid");

            if (!int.TryParse(user.FindFirst("UserId").Value, out int userId))
                throw new Exception("User data was not found, contact the site administration.");

            var claimsVm = await _cache.GetOrCreateAsync(userId,
                async (x) => await _userService.GetUserClaims(userId));

            if (!claimsVm.ClaimList.Select(x => x.ClaimName).Contains(_claim.ToString()))
            {
                throw new UnauthorizedException("Not authorized", "Userid");
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}