
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberShop.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public int UserId { get; set; } = 0;
        public string UserIp { get; set; }
        public int LangId { get; set; } = 1;

        public string Claims { get; set; }

        protected void SetUserInfo()
        {
            UserIp = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            string langIdStr = Request.Cookies["LangId"];
            LangId = string.IsNullOrWhiteSpace(langIdStr) ? 1 : int.Parse(langIdStr);

            if (!User.Identity.IsAuthenticated)
                return;
            
            if (!int.TryParse(User.FindFirst("UserId").Value, out int userId))
                throw new Exception("User data was not found, contact the site administration.");
            UserId = userId;
            //List<Claim> claims = JsonSerializer.Deserialize<List<Claim>>(User.FindFirst("Claims").Value);

            Claims = User.FindFirst("ClaimIds")?.Value;

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SetUserInfo();
            base.OnActionExecuting(context);
        }

    }
}
