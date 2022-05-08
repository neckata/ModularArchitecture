using Gamification.Shared.Core.Services.Identity;
using Gamification.Shared.DTOs.Identity.Tokens;
using Gamification.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gamification.Controllers
{
    [ApiVersion("1")]
    public class IdentityController : CommonBaseController
    {
        private readonly ITokenService _tokenService;

        public IdentityController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync(TokenRequest request)
        {
            var token = await _tokenService.GetTokenAsync(request, GenerateIPAddress());

            return Ok(token);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshAsync(RefreshTokenRequest request)
        {
            var response = await _tokenService.RefreshTokenAsync(request, GenerateIPAddress());

            return Ok(response);
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            }
        }
    }
}
