using OMS.BL.Models.Tables;
using OMS.Common.Extensions.Pagination;

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
        Task<PagedResult<TransactionModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves a transaction by its ID asynchronously.
        /// </summary>
        /// <param name="transactionId">The ID of the transaction.</param>
        /// <returns>The transaction model if found, otherwise null.</returns>
        Task<TransactionModel?> GetByIdAsync(int transactionId);
    }
}
