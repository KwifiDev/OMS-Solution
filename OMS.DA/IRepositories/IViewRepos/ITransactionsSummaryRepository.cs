using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface ITransactionsSummaryRepository
    {
        /// <summary>
        /// Retrieves all TransactionsSummary.
        /// </summary>
        /// <returns>The task result contains the collection of TransactionsSummary.</returns>
        Task<PagedResult<TransactionsSummary>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an TransactionsSummary by TransactionId.
        /// </summary>
        /// <param name="transactionId">The ID of the transaction.</param>
        /// <returns>The TransactionsSummary if found; otherwise, null.</returns>
        Task<TransactionsSummary?> GetByIdAsync(int transactionId);

        /// <summary>
        /// Retrieves all TransactionsSummary by Account Id.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The task result contains the collection of TransactionsSummary by Account Id.</returns>
        Task<PagedResult<TransactionsSummary>> GetTransactionsByAccountIdPagedAsync(int accountId, PaginationParams parameters);
    }
}
