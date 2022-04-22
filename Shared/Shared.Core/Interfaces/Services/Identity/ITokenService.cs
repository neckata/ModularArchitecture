using System.Threading.Tasks;
using Gamification.Shared.Core.Wrapper;
using Gamification.Shared.DTOs.Identity.Tokens;

namespace Gamification.Shared.Core.Interfaces.Services.Identity
{
    public interface ITokenService
    {
        Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    }
}