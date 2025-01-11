using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IUserAccountRepository : IGenericViewRepository<UserAccount>
    {
        /// <summary>
        /// Retrieves an UserAccount by AccountId.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The UserAccount if found; otherwise, null.</returns>
        Task<UserAccount?> GetUserAccountByIdAsync(int accountId);
    }
}
