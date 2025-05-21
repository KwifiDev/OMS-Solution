
using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Interfaces.Views
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
        Task<IEnumerable<TransactionsSummaryModel>> GetAllAsync();

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
        Task<IEnumerable<TransactionsSummaryModel>> GetTransactionsByAccountIdAsync(int accountId);
    }
}
