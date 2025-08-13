using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities;

namespace OMS.DA.Seeders.Data
{
    public class PermissionSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Permissions.AnyAsync())
                return;

            var permissions = new List<Permission>
            {
                new() { Name = "Dashboard.View"},

                new() { Name = "Revenues.View" },new() { Name = "Revenues.Add" },new() { Name = "Revenues.Edit" }, new() { Name = "Revenues.Delete" },
                new() { Name = "People.View" }, new() { Name = "People.Add" }, new() { Name = "People.Edit" }, new() { Name = "People.Delete" },
                new() { Name = "Branches.View" }, new() { Name = "Branches.Add" }, new() { Name = "Branches.Edit" }, new() { Name = "Branches.Delete" },

                new() { Name = "Users.View" }, new() { Name = "Users.Add" },new() { Name = "Users.Edit" }, new() { Name = "Users.Delete" },
                new() { Name = "Users.ChangePassword" }, new() { Name = "Users.ManageRoles" }, new() { Name = "Users.Activation" },

                new() { Name = "Roles.View" }, new() { Name = "Roles.Add" },new() { Name = "Roles.Edit" }, new() { Name = "Roles.Delete" },
                new() { Name = "RoleClaims.View" }, new() { Name = "RoleClaims.Add" }, new() { Name = "RoleClaims.Delete" },

                new() { Name = "Clients.View" }, new() { Name = "Clients.Add" }, new() { Name = "Clients.Edit" }, new() { Name = "Clients.Delete" },
                new() { Name = "Services.View" }, new() { Name = "Services.Add" }, new() { Name = "Services.Edit" }, new() { Name = "Services.Delete" },

                new() { Name = "Accounts.View" }, new() { Name = "Accounts.Deposit" }, new() { Name = "Accounts.Withdraw" }, new() { Name = "Accounts.Transfer" },
                new() { Name = "Debts.View" }, new() { Name = "Debts.Add" }, new() { Name = "Debts.Edit" }, new() { Name = "Debts.Delete" }, new() { Name = "Debts.Pay" },
                new() { Name = "Sales.View" }, new() { Name = "Sales.Add" }, new() { Name = "Sales.Edit" }, new() { Name = "Sales.Delete" },
            };

            await context.Permissions.AddRangeAsync(permissions);
            await context.SaveChangesAsync();
        }
    }
}
