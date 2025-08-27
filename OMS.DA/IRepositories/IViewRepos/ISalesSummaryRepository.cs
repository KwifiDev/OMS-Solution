using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface ISalesSummaryRepository
    {
        /// <summary>
        /// Retrieves all SalesSummary.
        /// </summary>
        /// <returns>The task result contains the collection of SalesSummary.</returns>
        Task<PagedResult<SalesSummary>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an SalesSummary by clientId.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The SalesSummary if found; otherwise, null.</returns>
        Task<PagedResult<SalesSummary>> GetByClientIdPagedAsync(int clientId, PaginationParams parameters);

        /// <summary>
        /// Retrieves an SalesSummary by SaleId.
        /// </summary>
        /// <param name="saleId">The ID of the Sale.</param>
        /// <returns>The SalesSummary if found; otherwise, null.</returns>
        Task<SalesSummary?> GetByIdAsync(int saleId);
    }
}
