using Microsoft.AspNetCore.Identity;
using ModularArchitecture.Shared.Core.Constants;
using ModularArchitecture.Shared.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Core.Helpers
{
    public static class ClaimsHelper
    {
        public static async Task<IdentityResult> AddPermissionClaimAsync(this RoleManager<Role> roleManager, Role role, string permission)
        {
            IList<Claim> allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                return await roleManager.AddClaimAsync(role, new(ApplicationClaimTypes.Permission, permission));
            }

            return IdentityResult.Failed();
        }
    }
}
