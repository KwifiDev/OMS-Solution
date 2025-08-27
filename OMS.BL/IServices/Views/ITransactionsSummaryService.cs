using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving transactions summary.
    /// </summary>
    public interface ITransactionsSummaryService
    {
        /// <summary>
        /// Retrieves all transactions summary asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of TransactionsSummaryModel.</returns>
        Task<PagedResult<TransactionsSummaryModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves a transaction summary by its ID asynchronously.
        /// </summary>
        /// <param name="transactionId">The ID of the transaction.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a TransactionsSummaryModel or null if not found.</returns>
        Task<TransactionsSummaryModel?> GetByIdAsync(int transactionId);


        /// <summary>
        /// Retrieves all TransactionsSummary by Account Id.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The task result contains the collection of TransactionsSummary by Account Id.</returns>
        Task<PagedResult<TransactionsSummaryModel>> GetTransactionsByAccountIdPagedAsync(int accountId, PaginationParams parameters);
    }
}
