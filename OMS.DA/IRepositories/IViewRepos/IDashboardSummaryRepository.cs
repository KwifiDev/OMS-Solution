using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IDashboardSummaryRepository
    {
        /// <summary>
        /// Retrieves all Dashboard Summary
        /// </summary>
        /// <returns>The task result contains the collection of DashboardSummary.</returns>
        Task<IEnumerable<DashboardSummary>> GetAllAsync();
    }
}
