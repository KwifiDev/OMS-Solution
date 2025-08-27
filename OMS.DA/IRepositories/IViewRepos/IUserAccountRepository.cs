using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IUserAccountRepository
    {
        /// <summary>
        /// Retrieves all UserAccount.
        /// </summary>
        /// <returns>The task result contains the collection of UserAccount.</returns>
        Task<PagedResult<UserAccount>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an UserAccount by AccountId.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The UserAccount if found; otherwise, null.</returns>
        Task<UserAccount?> GetByIdAsync(int accountId);
    }
}
