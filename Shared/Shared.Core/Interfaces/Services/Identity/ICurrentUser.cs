using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Shared.Core.Interfaces.Services.Identity
{
    public interface ICurrentUser
    {
        string Name { get; }

        Guid GetUserId();

        string GetUserEmail();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim> GetUserClaims();
    }
}
