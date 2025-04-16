
using OMS.Common.Enums;
using OMS.DA.Entities;

namespace OMS.DA.IRepositories.IEntityRepos
{
    /// <summary>
    /// Represents a repository for managing accounts.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Retrieves an account by client ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>An account Entity if found, otherwise null.</returns>
        Task<Account?> GetByClientIdAsync(int clientId);

        /// <summary>
        /// Deposits the specified amount into the account.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <param name="amount">The amount to deposit.</param>
        /// <param name="notes">Optional notes for the deposit.</param>
        /// <param name="createdByUserId">The ID of the user who created the deposit.</param>
        /// <returns>The transaction status of the deposit.</returns>
        Task<EnAccountTransactionStatus> DepositAccountAsync(int accountId, decimal amount, string? notes, int createdByUserId);

        /// <summary>
        /// Withdraws the specified amount from the account.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="notes">Optional notes for the withdrawal.</param>
        /// <param name="createdByUserId">The ID of the user who created the withdrawal.</param>
        /// <returns>The transaction status of the withdrawal.</returns>
        Task<EnAccountTransactionStatus> WithdrawAccountAsync(int accountId, decimal amount, string? notes, int createdByUserId);
    }
}
