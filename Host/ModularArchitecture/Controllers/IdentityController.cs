using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModularArchitecture.Shared.Core.Services.Identity;
using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Identity.Tokens;
using ModularArchitecture.Shared.Infrastructure.Controllers;
using System.Threading.Tasks;

namespace Host.ModularArchitecture.Controllers
{
    /// <summary>
    /// IdentityController
    /// </summary>
    [ApiVersion("1")]
    public class IdentityController : CommonBaseController
    {
        private readonly ITokenService _tokenService;

        /// <summary>
        /// IdentityController
        /// </summary>
        /// <param name="tokenService"></param>
        public IdentityController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Create token
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Generated token</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync(TokenRequest request)
        {
            IResult<TokenResponse> token = await _tokenService.GetTokenAsync(request, GenerateIPAddress());

            return Ok(token);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="request"></param>
        /// <returns>New token</returns>
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshAsync(RefreshTokenRequest request)
        {
            IResult<TokenResponse> response = await _tokenService.RefreshTokenAsync(request, GenerateIPAddress());

            return Ok(response);
        }

        /// <summary>
        /// GenerateIPAddress
        /// </summary>
        /// <returns>IPv4 IP Address</returns>
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
