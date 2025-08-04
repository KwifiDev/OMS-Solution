using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities.Identity;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.ViewRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class RoleClaimRepository : GenericViewRepository<RoleClaim>, IRoleClaimRepository
    {
        public RoleClaimRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RoleClaim>> GetRoleClaimsByRoleId(int roleId)
        {
            return await _dbSet.AsNoTracking()
                               .Where(rc => rc.RoleId == roleId)
                               .Select(rc => new RoleClaim 
                               {
                                   Id = rc.Id,
                                   ClaimType = rc.ClaimType,
                                   ClaimValue = rc.ClaimValue
                               })
                               .ToListAsync();
        }
    }
}
