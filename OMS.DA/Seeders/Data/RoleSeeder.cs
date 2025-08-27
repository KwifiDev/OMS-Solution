using Microsoft.AspNetCore.Identity;
using OMS.DA.Entities.Identity;

namespace OMS.DA.Seeders.Data
{
    public class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new Role { Name = "Admin" });

            if (!await roleManager.RoleExistsAsync("Employee"))
                await roleManager.CreateAsync(new Role { Name = "Employee" });

            if (!await roleManager.RoleExistsAsync("RoleManager"))
                await roleManager.CreateAsync(new Role { Name = "RoleManager" });
        }
    }
}
