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


        //Task<EnRoleResult> UpdateRoleClaimsAsync(int roleId, List<ClaimModel> claims);
        //Task<IList<Claim>> GetRoleClaimsAsync(int roleId);
        //Task<EnRoleResult> AssignPermissionsToRole(int roleId, IEnumerable<string> permissions);
        //Task<IEnumerable<string>> GetRolePermissions(int roleId);
        //Task<bool> HasPermissionAsync(int roleId, string permission);
    }
}
