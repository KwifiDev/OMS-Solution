using OMS.DA.Entities.Identity;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IRoleClaimRepository
    {
        Task<IEnumerable<RoleClaim>> GetAllAsync();
        Task<IEnumerable<RoleClaim>> GetRoleClaimsByRoleId(int roleId);
        Task<RoleClaim?> GetByIdAsync(int roleClaimId);
        
    }
}
