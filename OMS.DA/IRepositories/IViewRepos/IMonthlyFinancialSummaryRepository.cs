using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IMonthlyFinancialSummaryRepository
    {
        /// <summary>
        /// Retrieves all MonthlyFinancialSummary.
        /// </summary>
        /// <returns>The task result contains the collection of MonthlyFinancialSummary.</returns>
        Task<IEnumerable<MonthlyFinancialSummary>> GetAllAsync();
    }
}
