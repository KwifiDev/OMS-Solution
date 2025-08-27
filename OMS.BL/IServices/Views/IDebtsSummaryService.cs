using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for managing debts summary.
    /// </summary>
    public interface IDebtsSummaryService
    {
        /// <summary>
        /// Retrieves all debts summary asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of debts summary models.</returns>
        Task<PagedResult<DebtsSummaryModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an DebtsSummary Model by clientId.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The DebtsSummary Model if found; otherwise, null.</returns>
        Task<PagedResult<DebtsSummaryModel>> GetByClientIdPagedAsync(int clientId, PaginationParams parameters);

        /// <summary>
        /// Retrieves a debt summary by its ID asynchronously.
        /// </summary>
        /// <param name="debtId">The ID of the debt summary to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the debt summary model, or null if not found.</returns>
        Task<DebtsSummaryModel?> GetByIdAsync(int debtId);
    }
}
