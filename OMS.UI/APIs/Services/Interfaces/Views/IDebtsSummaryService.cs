using OMS.Common.Extensions.Pagination;
using OMS.UI.Models.Views;
using OMS.UI.Services.ModelTransfer;

namespace OMS.UI.APIs.Services.Interfaces.Views
{
    /// <summary>
    /// Represents a service for managing debts summary.
    /// </summary>
    public interface IDebtsSummaryService : IDisplayService<DebtsSummaryModel>
    {
        /// <summary>
        /// Retrieves all debts summary asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of debts summary models.</returns>
        //Task<IEnumerable<DebtsSummaryModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a debt summary by its ID asynchronously.
        /// </summary>
        /// <param name="debtId">The ID of the debt summary to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the debt summary model, or null if not found.</returns>
        //Task<DebtsSummaryModel?> GetByIdAsync(int debtId);

        /// <summary>
        /// Retrieves DebtsSummary by client asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of Debts Summary.</returns>
        Task<PagedResult<DebtsSummaryModel>?> GetDebtsByClientIdPagedAsync(int clientId, PaginationParams parameters);
    }
}
