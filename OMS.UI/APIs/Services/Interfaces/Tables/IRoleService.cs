using OMS.Common.Extensions.Pagination;
using OMS.UI.Models.Tables;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleModel>> GetAllAsync();
        Task<PagedResult<RoleModel>?> GetPagedAsync(PaginationParams parameters);
        Task<RoleModel?> GetByIdAsync(int id);
        Task<RoleModel?> GetByNameAsync(string roleName);
        Task<bool> AddAsync(RoleModel roleModel);
        Task<bool> UpdateAsync(int roleId, RoleModel roleModel);
        Task<bool> DeleteAsync(int roleId);

    }
}
