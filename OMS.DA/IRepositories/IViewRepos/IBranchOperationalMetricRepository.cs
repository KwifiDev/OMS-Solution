using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IBranchOperationalMetricRepository : IGenericViewRepository<BranchOperationalMetric>
    {
        /// <summary>
        /// Retrieves an BranchOperationalMetric by BranchId.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The BranchOperationalMetric if found; otherwise, null.</returns>
        Task<BranchOperationalMetric?> GetBranchOperationalMetricByIdAsync(int branchId);
    }
}
