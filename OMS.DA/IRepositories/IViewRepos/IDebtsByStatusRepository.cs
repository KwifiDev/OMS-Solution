using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IDebtsByStatusRepository
    {
        /// <summary>
        /// Retrieves all DebtsByStatus.
        /// </summary>
        /// <returns>The task result contains the collection of DebtsByStatus.</returns>
        Task<IEnumerable<DebtsByStatus>> GetAllAsync();
    }
}
