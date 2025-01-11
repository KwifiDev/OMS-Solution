using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    /// <summary>
    /// Represents a service for managing transactions.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Retrieves all transactions asynchronously.
        /// </summary>
        /// <returns>A collection of transaction models.</returns>
        Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync();

        /// <summary>
        /// Retrieves a transaction by its ID asynchronously.
        /// </summary>
        /// <param name="transactionId">The ID of the transaction.</param>
        /// <returns>The transaction model if found, otherwise null.</returns>
        Task<TransactionModel?> GetTransactionByIdAsync(int transactionId);
    }
}
