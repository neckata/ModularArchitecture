using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Identity.Tokens;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Core.Services.Identity
{
    public interface ITokenService
    {
        Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    }
}