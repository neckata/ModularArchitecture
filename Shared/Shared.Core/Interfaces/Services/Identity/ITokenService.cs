using Shared.Core.Wrapper;
using Shared.DTOs.Identity.Tokens;
using System.Threading.Tasks;

namespace Shared.Core.Interfaces.Services.Identity
{
    public interface ITokenService
    {
        Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    }
}
