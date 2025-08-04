using OMS.UI.Models.Tables;
using OMS.UI.Services.ModelTransfer;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    public interface IRoleClaimService : IDisplayService<RoleClaimModel>
    {
        Task<IEnumerable<RoleClaimModel>> GetRoleClaimsByRoleIdAsync(int roleId);
        Task<bool> AddRoleClaimAsync(int roleId, string claimType, string claimValue);
        Task<bool> RemoveRoleClaimAsync(int roleId, string claimType, string claimValue);
    }
}
