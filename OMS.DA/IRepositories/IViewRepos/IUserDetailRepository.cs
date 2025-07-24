using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IUserDetailRepository
    {
        /// <summary>
        /// Retrieves all UserDetail.
        /// </summary>
        /// <returns>The task result contains the collection of UserDetail.</returns>
        Task<IEnumerable<UserDetail>> GetAllAsync();

        /// <summary>
        /// Retrieves an UserDetail by Id.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The UserDetail if found; otherwise, null.</returns>
        Task<UserDetail?> GetByIdAsync(int userId);
    }
}
