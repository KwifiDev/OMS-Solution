using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Tables
{
    /// <summary>
    /// Represents the interface for managing accounts.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Retrieves all accounts asynchronously.
        /// </summary>
        /// <returns>A collection of account models.</returns>
        Task<PagedResult<AccountModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an account by its ID asynchronously.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>An account model if found, otherwise null.</returns>
        Task<AccountModel?> GetByIdAsync(int accountId);

        /// <summary>
        /// find a account by their ID asynchronously.
        /// </summary>
        /// <param name="accountId">The ID of the account to find.</param>
        /// <returns>True if the account was exist, otherwise false.</returns>
        Task<bool> IsExistAsync(int accountId);

        /// <summary>
        /// Retrieves an account by client ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>An account model if found, otherwise null.</returns>
        Task<AccountModel?> GetByClientIdAsync(int clientId);

        /// <summary>
        /// Adds a new account asynchronously.
        /// </summary>
        /// <param name="model">The account model to add.</param>
        /// <returns>True if the account was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(AccountModel model);

        /// <summary>
        /// Updates an existing account asynchronously.
        /// </summary>
        /// <param name="model">The account model to update.</param>
        /// <returns>True if the account was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(AccountModel model);

        /// <summary>
        /// Deletes an account by its ID asynchronously.
        /// </summary>
        /// <param name="accountId">The ID of the account to delete.</param>
        /// <returns>True if the account was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int accountId);

        /// <summary>
        /// make transaction to an account asynchronously.
        /// </summary>
        /// <param name="model">The account transaction model.</param>
        /// <returns>True if the transaction was successful, otherwise false.</returns>
        Task<bool> StartTransactionAsync(AccountTransactionModel model);
    }
}
