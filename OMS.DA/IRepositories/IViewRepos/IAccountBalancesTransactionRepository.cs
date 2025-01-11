using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IAccountBalancesTransactionRepository : IGenericViewRepository<AccountBalancesTransaction>
    {
        /// <summary>
        /// Retrieves an AccountBalancesTransaction by AccountId.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The AccountBalancesTransaction if found; otherwise, null.</returns>
        Task<AccountBalancesTransaction?> GetAccountBalancesTransactionByIdAsync(int accountId);
    }
}
