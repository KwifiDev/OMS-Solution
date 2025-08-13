using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.DA.Entities.Identity;

namespace OMS.DA.Seeders.Data
{
    public class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> roleManager)
        {
            if (await roleManager.Roles.AnyAsync()) return;

            var result = await roleManager.CreateAsync(new Role { Name = "Admin" });

            if (!result.Succeeded) return;
        }
    }
}
