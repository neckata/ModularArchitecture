using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ModularArchitecture.Shared.Core.Constants;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Helpers;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Interfaces.Services;
using ModularArchitecture.Shared.Infrastructure.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Infrastructure.Persistence
{
    internal class IdentityDbSeeder : IDatabaseSeeder
    {
        private readonly ILogger<IdentityDbSeeder> _logger;
        private readonly IApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public IdentityDbSeeder(
            ILogger<IdentityDbSeeder> logger,
            IApplicationDbContext db,
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            _logger = logger;
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            AddDefaultRoles();
            AddSuperAdmin();
            _db.SaveChanges();
        }

        private void AddDefaultRoles()
        {
            Task.Run(async () =>
            {
                List<string> roleList = new List<string> { RoleConstants.SuperAdmin, RoleConstants.Admin };
                foreach (string roleName in roleList)
                {
                    Role role = new Role(roleName);
                    Role roleInDb = await _roleManager.FindByNameAsync(roleName);
                    if (roleInDb == null)
                    {
                        await _roleManager.CreateAsync(role);
                        _logger.LogInformation(string.Format("Added '{0}' to Roles", roleName));
                    }
                }
            }).GetAwaiter().GetResult();
        }

        private void AddSuperAdmin()
        {
            Task.Run(async () =>
            {
                // Check if Role Exists
                Role superAdminRole = new Role(RoleConstants.SuperAdmin);
                Role superAdminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.SuperAdmin);
                if (superAdminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(superAdminRole);
                    superAdminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.SuperAdmin);
                }

                // Check if User Exists
                User superUser = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@admin.com",
                    UserName = "admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };
                User superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                    IdentityResult result = await _userManager.AddToRoleAsync(superUser, RoleConstants.SuperAdmin);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Seeded Default SuperAdmin User.");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }

                foreach (string permission in typeof(Core.Constants.Permissions).GetNestedClassesStaticStringValues())
                {
                    await _roleManager.AddPermissionClaimAsync(superAdminRoleInDb, permission);
                }
            }).GetAwaiter().GetResult();
        }
    }
}
