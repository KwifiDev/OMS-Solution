using OMS.BL.Models.Views;

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
        Task<IEnumerable<TransactionsSummaryModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a transaction summary by its ID asynchronously.
        /// </summary>
        /// <param name="transactionId">The ID of the transaction.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a TransactionsSummaryModel or null if not found.</returns>
        Task<TransactionsSummaryModel?> GetByIdAsync(int transactionId);
    }
}
