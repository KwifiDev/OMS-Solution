using OMS.BL.Models.Tables;
using OMS.Common.Enums;

namespace OMS.BL.IServices.Tables
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleModel>> GetAllAsync();

        Task<RoleModel?> FindByIdAsync(int roleId);

        Task<RoleModel?> FindByNameAsync(string roleName);

        Task<EnRoleResult> AddAsync(RoleModel model);

        Task<EnRoleResult> UpdateAsync(RoleModel model);

        Task<EnRoleResult> DeleteAsync(int roleId);

        Task<bool> IsExists(int roleId);

        Task<IEnumerable<string>> GetClaimsAsync(string roleName);
    }
}
