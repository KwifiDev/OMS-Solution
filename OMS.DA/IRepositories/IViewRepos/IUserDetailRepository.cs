using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IUserDetailRepository : IGenericViewRepository<UserDetail>
    {
        /// <summary>
        /// Retrieves an UserDetail by UserId.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The UserDetail if found; otherwise, null.</returns>
        Task<UserDetail?> GetUserDetailByIdAsync(int userId);
    }
}
