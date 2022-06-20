﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Exceptions;
using ModularArchitecture.Shared.Core.Interfaces.Services;
using ModularArchitecture.Shared.Core.Services.Identity;
using ModularArchitecture.Shared.Core.Settings;
using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Identity.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtSettings _config;
        private readonly IEventLogService _eventLog;

        public TokenService(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<JwtSettings> config,
            IEventLogService eventLog)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config.Value;
            _eventLog = eventLog;
        }

        public async Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress)
        {
            User user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new IdentityException("User Not Found.", statusCode: HttpStatusCode.Unauthorized);
            }

            if (!user.IsActive)
            {
                throw new IdentityException("User Not Active. Please contact the administrator.", statusCode: HttpStatusCode.Unauthorized);
            }

            bool passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                throw new IdentityException("Invalid Credentials.", statusCode: HttpStatusCode.Unauthorized);
            }

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_config.RefreshTokenExpirationInDays);
            await _userManager.UpdateAsync(user);
            string token = await GenerateJwtAsync(user, ipAddress);
            TokenResponse response = new TokenResponse(token, user.RefreshToken, user.RefreshTokenExpiryTime);
            await _eventLog.LogCustomEventAsync(new() { Description = $"Generated Tokens for {user.Email}." });
            return await Result<TokenResponse>.SuccessAsync(response);
        }

        public async Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
        {
            if (request is null)
            {
                throw new IdentityException("Invalid Client Token.", statusCode: HttpStatusCode.Unauthorized);
            }

            ClaimsPrincipal userPrincipal = GetPrincipalFromExpiredToken(request.Token);
            string userEmail = userPrincipal.FindFirstValue(ClaimTypes.Email);
            User user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                throw new IdentityException("User Not Found.", statusCode: HttpStatusCode.NotFound);
            }

            if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new IdentityException("Invalid Client Token.", statusCode: HttpStatusCode.Unauthorized);
            }

            string token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user, ipAddress));
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_config.RefreshTokenExpirationInDays);
            await _userManager.UpdateAsync(user);
            TokenResponse response = new TokenResponse(token, user.RefreshToken, user.RefreshTokenExpiryTime);
            return await Result<TokenResponse>.SuccessAsync(response);
        }

        private async Task<string> GenerateJwtAsync(User user, string ipAddress)
        {
            return GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user, ipAddress));
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user, string ipAddress)
        {
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim> roleClaims = new List<Claim>();
            List<Claim> permissionClaims = new List<Claim>();
            foreach (string role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
                Role thisRole = await _roleManager.FindByNameAsync(role);
                IList<Claim> allPermissionsForThisRoles = await _roleManager.GetClaimsAsync(thisRole);
                permissionClaims.AddRange(allPermissionsForThisRoles);
            }

            return new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
                new("fullName", $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Surname, user.LastName),
                new("ipAddress", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);
        }

        private string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            JwtSecurityToken token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(_config.TokenExpirationInMinutes),
               signingCredentials: signingCredentials);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new IdentityException("Invalid Token.", statusCode: HttpStatusCode.Unauthorized);
            }

            return principal;
        }

        private SigningCredentials GetSigningCredentials()
        {
            byte[] secret = Encoding.UTF8.GetBytes(_config.Key);
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }
    }
}