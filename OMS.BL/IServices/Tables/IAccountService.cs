using OMS.BL.Models.DTOs_StoredProcedures;
using OMS.BL.Models.Tables;

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
        Task<IEnumerable<AccountModel>> GetAllAccountsAsync();

        /// <summary>
        /// Retrieves an account by its ID asynchronously.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>An account model if found, otherwise null.</returns>
        Task<AccountModel?> GetAccountByIdAsync(int accountId);

        /// <summary>
        /// Adds a new account asynchronously.
        /// </summary>
        /// <param name="model">The account model to add.</param>
        /// <returns>True if the account was added successfully, otherwise false.</returns>
        Task<bool> AddAccountAsync(AccountModel model);

        /// <summary>
        /// Updates an existing account asynchronously.
        /// </summary>
        /// <param name="model">The account model to update.</param>
        /// <returns>True if the account was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAccountAsync(AccountModel model);

        /// <summary>
        /// Deletes an account by its ID asynchronously.
        /// </summary>
        /// <param name="accountId">The ID of the account to delete.</param>
        /// <returns>True if the account was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAccountAsync(int accountId);

        /// <summary>
        /// Deposits an amount into an account asynchronously.
        /// </summary>
        /// <param name="model">The account transaction model.</param>
        /// <returns>True if the deposit was successful, otherwise false.</returns>
        Task<bool> DepositIntoAccountAsync(AccountTransactionModel model);

        /// <summary>
        /// Withdraws an amount from an account asynchronously.
        /// </summary>
        /// <param name="model">The account transaction model.</param>
        /// <returns>True if the withdrawal was successful, otherwise false.</returns>
        Task<bool> WithdrawFromAccountAsync(AccountTransactionModel model);
    }
}
