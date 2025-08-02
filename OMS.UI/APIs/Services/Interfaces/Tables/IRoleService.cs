using OMS.UI.Models.Tables;
using System.Security.Claims;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleModel>> GetAllAsync();
        Task<RoleModel?> GetByIdAsync(int id);
        Task<RoleModel?> GetByNameAsync(string roleName);
        Task<bool> AddAsync(RoleModel roleModel);
        Task<bool> UpdateAsync(int roleId, RoleModel roleModel);
        Task<bool> DeleteAsync(int roleId);
        Task<bool> AddRoleClaimAsync(int roleId, Claim claim);
        Task<bool> RemoveRoleClaimAsync(int roleId, Claim claim);

    }
}
