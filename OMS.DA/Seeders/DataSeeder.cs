using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities.Identity;
using OMS.DA.Seeders.Data;

namespace OMS.DA.Seeders
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(AppDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
                await RoleSeeder.SeedAsync(roleManager);
                await PermissionSeeder.SeedAsync(context);
                await RoleClaimSeeder.SeedAsync(context, roleManager);
                await UserSeeder.SeedAsync(context, userManager);
            }
        }
    }
}
