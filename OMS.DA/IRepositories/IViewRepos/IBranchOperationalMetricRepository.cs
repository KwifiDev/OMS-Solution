using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IBranchOperationalMetricRepository
    {
        /// <summary>
        /// Retrieves all BranchOperationalMetric.
        /// </summary>
        /// <returns>The task result contains the collection of BranchOperationalMetric.</returns>
        Task<IEnumerable<BranchOperationalMetric>> GetAllAsync();

        /// <summary>
        /// Retrieves an BranchOperationalMetric by BranchId.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The BranchOperationalMetric if found; otherwise, null.</returns>
        Task<BranchOperationalMetric?> GetByIdAsync(int branchId);
    }
}
