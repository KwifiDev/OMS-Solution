using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving account balances transactions.
    /// </summary>
    public interface IAccountBalancesTransactionService
    {
        /// <summary>
        /// Retrieves all account balances transactions asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of account balances transactions.</returns>
        Task<PagedResult<AccountBalancesTransactionModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an account balances transaction by its ID asynchronously.
        /// </summary>
        /// <param name="accountId">The ID of the account balances transaction to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the account balances transaction, or null if not found.</returns>
        Task<AccountBalancesTransactionModel?> GetByIdAsync(int accountId);
    }
}
