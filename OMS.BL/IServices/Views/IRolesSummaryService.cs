using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    public interface IRolesSummaryService
    {
        /// <summary>
        /// Retrieves all roles asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of roles.</returns>
        Task<PagedResult<RolesSummaryModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an roles summary by its ID asynchronously.
        /// </summary>
        /// <param name="roleId">The ID of the role to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains roles summary, or null if not found.</returns>
        Task<RolesSummaryModel?> GetByIdAsync(int roleId);
    }
}
