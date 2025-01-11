using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IDebtsSummaryRepository : IGenericViewRepository<DebtsSummary>
    {
        /// <summary>
        /// Retrieves an DebtsSummary by debtId.
        /// </summary>
        /// <param name="debtId">The ID of the debt.</param>
        /// <returns>The DebtSummary if found; otherwise, null.</returns>
        Task<DebtsSummary?> GetDebtSummaryByIdAsync(int debtId);
    }
}
