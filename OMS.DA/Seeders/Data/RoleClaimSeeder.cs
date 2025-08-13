using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities.Identity;
using System.Security.Claims;

namespace OMS.DA.Seeders.Data
{
    public class RoleClaimSeeder
    {
        public static async Task SeedAsync(AppDbContext context, RoleManager<Role> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole is null) return;

            var roleClaims = await roleManager.GetClaimsAsync(adminRole);
            if (roleClaims.Count > 0) return;

            var permissions = await context.Permissions.Select(p => p.Name).ToListAsync();
            if (permissions.Count == 0) return;

            foreach (var permission in permissions)
            {
                await roleManager.AddClaimAsync(adminRole, new Claim("Permission", permission));
            }
        }
    }
}
