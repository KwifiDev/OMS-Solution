using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.Entities.Identity;

namespace OMS.DA.Seeders.Data
{
    public class UserSeeder
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<User> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var person = new Person { FirstName = "FirstNameHere", LastName = "LastNameHere", Gender = EnGender.Male };
            if (!await context.People.AnyAsync())
            {
                await context.People.AddAsync(person);
            }

            var branch = new Branch { Name = "BranchNameHere", Address = "BranchAddressHere.............." };
            if (!await context.Branches.AnyAsync())
            {
                await context.Branches.AddAsync(branch);
            }

            await context.SaveChangesAsync();

            var adminUser = new User
            {
                PersonId = await context.People.Where(p => p.FirstName == "FirstNameHere").Select(p => p.Id).FirstOrDefaultAsync(),
                BranchId = await context.Branches.Where(b => b.Name == "BranchNameHere").Select(b => b.Id).FirstOrDefaultAsync(),
                UserName = "Admin",
                IsActive = true
            };

            var createUserResult = await userManager.CreateAsync(adminUser, "Admin.123");

            if (!createUserResult.Succeeded) return;

            var addUserRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");

            if (!addUserRoleResult.Succeeded) return;
        }
    }
}
