using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving branch operational metric data.
    /// </summary>
    public interface IBranchOperationalMetricService
    {
        /// <summary>
        /// Retrieves all branch operational metric records asynchronously.
        /// </summary>
        /// <returns>A collection of branch operational metric models.</returns>
        Task<PagedResult<BranchOperationalMetricModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves a branch operational metric record by its ID asynchronously.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The branch operational metric model, or null if not found.</returns>
        Task<BranchOperationalMetricModel?> GetByIdAsync(int branchId);
    }
}
