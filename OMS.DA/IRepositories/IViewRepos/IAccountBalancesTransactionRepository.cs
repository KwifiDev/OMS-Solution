using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IAccountBalancesTransactionRepository
    {
        /// <summary>
        /// Retrieves all AccountBalancesTransactions.
        /// </summary>
        /// <returns>The task result contains the collection of AccountBalancesTransactions.</returns>
        Task<IEnumerable<AccountBalancesTransaction>> GetAllAsync();

        /// <summary>
        /// Retrieves an AccountBalancesTransaction by AccountId.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The AccountBalancesTransaction if found; otherwise, null.</returns>
        Task<AccountBalancesTransaction?> GetByIdAsync(int accountId);

    }
}
