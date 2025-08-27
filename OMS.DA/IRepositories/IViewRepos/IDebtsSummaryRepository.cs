using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IDebtsSummaryRepository
    {
        /// <summary>
        /// Retrieves all DebtsSummary.
        /// </summary>
        /// <returns>The task result contains the collection of DebtsSummary.</returns>
        Task<PagedResult<DebtsSummary>> GetPagedAsync(PaginationParams parammeters);

        /// <summary>
        /// Retrieves an DebtsSummary by clientId.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The DebtsSummary if found; otherwise, null.</returns>
        Task<PagedResult<DebtsSummary>> GetByClientIdPagedAsync(int clientId, PaginationParams parammeters);

        /// <summary>
        /// Retrieves an DebtsSummary by debtId.
        /// </summary>
        /// <param name="debtId">The ID of the debt.</param>
        /// <returns>The DebtSummary if found; otherwise, null.</returns>
        Task<DebtsSummary?> GetByIdAsync(int debtId);
    }
}
