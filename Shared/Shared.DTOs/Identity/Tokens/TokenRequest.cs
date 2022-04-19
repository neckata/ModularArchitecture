using System;

namespace Shared.DTOs.Identity.Tokens
{
    public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
}
