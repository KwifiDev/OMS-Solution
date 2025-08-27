using OMS.Common.Extensions.Pagination;
using OMS.DA.Entities.Identity;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IRoleClaimRepository
    {
        Task<PagedResult<RoleClaim>> GetPagedAsync(PaginationParams parameters);
        Task<IEnumerable<RoleClaim>> GetRoleClaimsByRoleId(int roleId);
        Task<RoleClaim?> GetByIdAsync(int roleClaimId);
        
    }
}
