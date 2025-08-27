using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for managing discounts applied.
    /// </summary>
    public interface IDiscountsAppliedService
    {
        /// <summary>
        /// Retrieves all discounts applied asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of discounts applied.</returns>
        Task<PagedResult<DiscountsAppliedModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an DiscountsApplied Model by serviceId.
        /// </summary>
        /// <param name="serviceId">The ID of the service.</param>
        /// <returns>The DiscountsApplied Model if found; otherwise, null.</returns>
        Task<PagedResult<DiscountsAppliedModel>> GetByServiceIdPagedAsync(int serviceId, PaginationParams parameters);

        /// <summary>
        /// Retrieves a discount applied by its ID asynchronously.
        /// </summary>
        /// <param name="discountId">The ID of the discount applied.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the discount applied with the specified ID, or null if not found.</returns>
        Task<DiscountsAppliedModel?> GetByIdAsync(int discountId);
    }
}
