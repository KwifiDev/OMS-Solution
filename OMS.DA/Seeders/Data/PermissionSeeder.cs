using Microsoft.EntityFrameworkCore;
using OMS.Common.Data;
using OMS.DA.Context;
using OMS.DA.Entities;

namespace OMS.DA.Seeders.Data
{
    public class PermissionSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var dbPermissions = await context.Permissions.Select(p => p.Name).ToListAsync();
            var seedPermissions = PermissionsData.GetAll().Select(permissionName => new Permission { Name = permissionName });

            var permissionsToAdd = seedPermissions.Where(seedPerm => !dbPermissions.Contains(seedPerm.Name)).ToList();

            if (permissionsToAdd.Count != 0)
            {
                await context.Permissions.AddRangeAsync(permissionsToAdd);
                await context.SaveChangesAsync();
            }
        }
    }
}
