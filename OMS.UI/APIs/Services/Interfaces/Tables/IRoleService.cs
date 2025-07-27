using OMS.UI.Models.Tables;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleModel>> GetAllAsync();
        Task<RoleModel?> GetByIdAsync(int roleId);
        Task<RoleModel?> GetByNameAsync(string roleName);
        Task<bool> AddAsync(RoleModel roleModel);
        Task<bool> UpdateAsync(int roleId, RoleModel roleModel);
        Task<bool> DeleteAsync(int roleId);
    }
}
