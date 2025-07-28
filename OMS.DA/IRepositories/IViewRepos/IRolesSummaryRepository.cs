using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IRolesSummaryRepository
    {
        /// <summary>
        /// Retrieves all RolesSummary.
        /// </summary>
        /// <returns>The task result contains the collection of RolesSummary.</returns>
        Task<IEnumerable<RolesSummary>> GetAllAsync();

        /// <summary>
        /// Retrieves an RolesSummary by roleId.
        /// </summary>
        /// <param name="roleId">The ID of the role.</param>
        /// <returns>The RolesSummary if found; otherwise, null.</returns>
        Task<RolesSummary?> GetByIdAsync(int roleId);
    }
}
