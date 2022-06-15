using ModularArchitecture.Shared.Core.Services.Identity;
using ModularArchitecture.Shared.DTOs.Identity.Tokens;
using ModularArchitecture.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ModularArchitecture.Shared.Core.Wrapper;

namespace Host.ModularArchitecture.Controllers
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
            IResult<TokenResponse> token = await _tokenService.GetTokenAsync(request, GenerateIPAddress());

            return Ok(token);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshAsync(RefreshTokenRequest request)
        {
            IResult<TokenResponse> response = await _tokenService.RefreshTokenAsync(request, GenerateIPAddress());

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
