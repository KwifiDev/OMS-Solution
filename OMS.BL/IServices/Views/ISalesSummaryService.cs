using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving sales summary information.
    /// </summary>
    public interface ISalesSummaryService
    {
        /// <summary>
        /// Retrieves all sales summary asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of sales summary models.</returns>
        Task<PagedResult<SalesSummaryModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an SalesSummary Model by clientId.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The SalesSummary Model if found; otherwise, null.</returns>
        Task<PagedResult<SalesSummaryModel>> GetByClientIdPagedAsync(int clientId, PaginationParams parameters);

        /// <summary>
        /// Retrieves a sales summary by its ID asynchronously.
        /// </summary>
        /// <param name="saleId">The ID of the sales summary to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the sales summary model, or null if not found.</returns>
        Task<SalesSummaryModel?> GetByIdAsync(int saleId);
    }
}
