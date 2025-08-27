using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities.Identity;
using System.Security.Claims;
using static OMS.Common.Data.PermissionsData;

namespace OMS.DA.Seeders.Data
{
    public class RoleClaimSeeder
    {
        public static async Task SeedAsync(AppDbContext context, RoleManager<Role> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole != null)
            {
                var adminClaims = await roleManager.GetClaimsAsync(adminRole);
                if (adminClaims.Count == 0)
                {
                    var allPermissions = await context.Permissions.Select(p => p.Name).ToListAsync();
                    if (allPermissions.Count > 0)
                    {
                        await AddClaimsToRole(roleManager, adminRole, allPermissions);
                    }
                }
            }

            var employeeRole = await roleManager.FindByNameAsync("Employee");
            if (employeeRole != null)
            {
                var employeeClaims = await roleManager.GetClaimsAsync(employeeRole);
                if (employeeClaims.Count == 0)
                {
                    var employeePermissions = new List<string>
                    {
                        Accounts.Transaction, Accounts.View, Branches.View,
                        Clients.Add, Clients.PayDebts, Clients.View,
                        ClientsSummary.View, Debts.Add, Debts.Cancel,
                        Debts.Edit, Debts.Pay, Debts.View,
                        DebtsSummary.View, Discounts.View, DiscountsApplied.View,
                        PaymentsSummary.View, People.View, Sales.Add,
                        Sales.Cancel, Sales.Edit, Sales.View,
                        SalesSummary.View, Services.Add, Services.View,
                        ServicesSummary.View, Transactions.View, TransactionsSummary.View,
                        Users.View, UsersAccount.View
                    };

                    var existingPermissions = await context.Permissions
                        .Where(p => employeePermissions.Contains(p.Name))
                        .Select(p => p.Name)
                        .ToListAsync();

                    if (existingPermissions.Count > 0)
                    {
                        await AddClaimsToRole(roleManager, employeeRole, existingPermissions);
                    }
                }
            }


            var roleManagerRole = await roleManager.FindByNameAsync("RoleManager");
            if (roleManagerRole != null)
            {
                var roleManagerClaims = await roleManager.GetClaimsAsync(roleManagerRole);
                if (roleManagerClaims.Count == 0)
                {
                    var roleManagerPermissions = new List<string>
                    {
                       Permissions.View, RoleClaims.Add, RoleClaims.Delete,
                       RoleClaims.View, Roles.Add, Roles.Delete, Roles.Edit,
                       Roles.View, RolesSummary.View, Users.ManageRoles,
                       Users.View, UsersDetail.View
                    };

                    var existingPermissions = await context.Permissions
                        .Where(p => roleManagerPermissions.Contains(p.Name))
                        .Select(p => p.Name)
                        .ToListAsync();

                    if (existingPermissions.Count > 0)
                    {
                        await AddClaimsToRole(roleManager, roleManagerRole, existingPermissions);
                    }
                }
            }

        }

        private static async Task AddClaimsToRole(RoleManager<Role> roleManager, Role role, IEnumerable<string> permissions)
        {
            foreach (var permission in permissions)
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}