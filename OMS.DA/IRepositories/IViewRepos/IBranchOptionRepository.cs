using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IBranchOptionRepository
    {
        /// <summary>
        /// Retrieves all BranchOption.
        /// </summary>
        /// <returns>The task result contains the collection of BranchOption.</returns>
        Task<IEnumerable<BranchOption>> GetAllAsync();

        /// <summary>
        /// Retrieves an BranchOption by BranchId.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The BranchOption if found; otherwise, null.</returns>
        Task<BranchOption?> GetByIdAsync(int branchId);
    }
}
