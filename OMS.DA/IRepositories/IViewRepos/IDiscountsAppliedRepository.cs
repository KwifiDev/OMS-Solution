using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IDiscountsAppliedRepository
    {
        /// <summary>
        /// Retrieves all DiscountsApplied.
        /// </summary>
        /// <returns>The task result contains the collection of DiscountsApplied.</returns>
        Task<PagedResult<DiscountsApplied>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an DiscountsApplied by serviceId.
        /// </summary>
        /// <param name="serviceId">The ID of the service.</param>
        /// <returns>The DiscountsApplied if found; otherwise, null.</returns>
        Task<PagedResult<DiscountsApplied>> GetByServiceIdPagedAsync(int serviceId, PaginationParams parameters);

        /// <summary>
        /// Retrieves an DiscountsApplied by DiscountId.
        /// </summary>
        /// <param name="discountId">The ID of the discount.</param>
        /// <returns>The DiscountsApplied if found; otherwise, null.</returns>
        Task<DiscountsApplied?> GetByIdAsync(int discountId);
    }
}
