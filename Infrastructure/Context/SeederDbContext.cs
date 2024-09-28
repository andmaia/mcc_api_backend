using Common.Authorization;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class SeederDbContext
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public SeederDbContext(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }



        public async Task SeedDatabaseAsync()
        {
            await CheckAndApplyPendingMigrationAsync();
            await SeedRolesAsync();
            await SeedBasicUserAsync();
            await SeedAdminUserAsync();
        }


        private async Task CheckAndApplyPendingMigrationAsync()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }

        private async Task SeedBasicUserAsync()
        {
            var basicUser = new ApplicationUser
            {
           
                Email = "johnd@abc.com",
                UserName = "johnd",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = "JOHND@ABC.COM",
                NormalizedUserName = "JOHND",
                IsActive = true
            };

            if (!await _userManager.Users.AnyAsync(u => u.Email == "johnd@abc.com"))
            {
                var password = new PasswordHasher<ApplicationUser>();
                basicUser.PasswordHash = password.HashPassword(basicUser, AppCredentials.Password);
                await _userManager.CreateAsync(basicUser);
            }

            // Assign role to user
            if (!await _userManager.IsInRoleAsync(basicUser, AppRole.Basic))
            {
                await _userManager.AddToRoleAsync(basicUser, AppRole.Basic);
            }
        }



        private async Task SeedAdminUserAsync()
        {
            string adminUserName = AppCredentials.Email[..AppCredentials.Email.IndexOf('@')].ToLowerInvariant();
            var adminUser = new ApplicationUser
            {
           
                Email = AppCredentials.Email,
                UserName = adminUserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = AppCredentials.Email.ToUpperInvariant(),
                NormalizedUserName = adminUserName.ToUpperInvariant(),
                IsActive = true
            };

            if (!await _userManager.Users.AnyAsync(u => u.Email == AppCredentials.Email))
            {
                var password = new PasswordHasher<ApplicationUser>();
                adminUser.PasswordHash = password.HashPassword(adminUser, AppCredentials.Password);
                await _userManager.CreateAsync(adminUser);
            }

            // Assign role to user
            if (!await _userManager.IsInRoleAsync(adminUser, AppRole.Basic)
                && !await _userManager.IsInRoleAsync(adminUser, AppRole.Admin))
            {
                await _userManager.AddToRolesAsync(adminUser, AppRole.DefaultRoles);
            }
        }


        private async Task SeedRolesAsync()
        {
            foreach (var roleName in AppRole.DefaultRoles)
            {
                if (await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName)
                    is not ApplicationRole role)
                {
                    role = new ApplicationRole
                    {
                        Name = roleName,
                        Description = $"{roleName} Role."
                    };

                    await _roleManager.CreateAsync(role);
                }

                if (roleName == AppRole.Admin)
                {
                    await AssignPermissionsToRoleAsync(role, AppPermissions.AdminPermissions);
                }
                else if (roleName == AppRole.Basic)
                {
                    await AssignPermissionsToRoleAsync(role, AppPermissions.BasicPermissions);
                }
                else if (roleName == AppRole.Manager)
                {
                    await AssignPermissionsToRoleAsync(role, AppPermissions.ManagerPermissions);
                }
            }
        }


        private async Task AssignPermissionsToRoleAsync(ApplicationRole role, IReadOnlyList<AppPermission> permissions)
        {


            var currentClaims = await _roleManager.GetClaimsAsync(role);
            foreach (var permission in permissions)
            {
                if (!currentClaims.Any(claim => claim.Type == AppClaim.Permission && claim.Value == permission.Name))
                {
                    await _dbContext.RoleClaims.AddAsync(new ApplicationRoleClaim
                    {
                        RoleId = role.Id,
                        ClaimType = AppClaim.Permission,
                        ClaimValue = permission.Name,
                        Description = permission.Description,
                        Group = permission.Group
                    });;

                    await _dbContext.SaveChangesAsync();
                }
            }
        }

    }
}
