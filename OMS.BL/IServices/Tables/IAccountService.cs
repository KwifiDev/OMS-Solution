using OMS.BL.Dtos.StoredProcedureParams;
using OMS.BL.Dtos.Tables;

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
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();

        /// <summary>
        /// Retrieves an account by its ID asynchronously.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>An account dto if found, otherwise null.</returns>
        Task<AccountDto?> GetAccountByIdAsync(int accountId);

        /// <summary>
        /// Adds a new account asynchronously.
        /// </summary>
        /// <param name="dto">The account dto to add.</param>
        /// <returns>True if the account was added successfully, otherwise false.</returns>
        Task<bool> AddAccountAsync(AccountDto dto);

        /// <summary>
        /// Updates an existing account asynchronously.
        /// </summary>
        /// <param name="dto">The account dto to update.</param>
        /// <returns>True if the account was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAccountAsync(AccountDto dto);

        /// <summary>
        /// Deletes an account by its ID asynchronously.
        /// </summary>
        /// <param name="accountId">The ID of the account to delete.</param>
        /// <returns>True if the account was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAccountAsync(int accountId);

        /// <summary>
        /// Deposits an amount into an account asynchronously.
        /// </summary>
        /// <param name="dto">The account transaction dto.</param>
        /// <returns>True if the deposit was successful, otherwise false.</returns>
        Task<bool> DepositIntoAccountAsync(AccountTransactionDto dto);

        /// <summary>
        /// Withdraws an amount from an account asynchronously.
        /// </summary>
        /// <param name="dto">The account transaction dto.</param>
        /// <returns>True if the withdrawal was successful, otherwise false.</returns>
        Task<bool> WithdrawFromAccountAsync(AccountTransactionDto dto);
    }
}
