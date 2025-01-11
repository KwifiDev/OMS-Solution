using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class MonthlyFinancialSummaryService : IMonthlyFinancialSummaryService
    {
        private readonly IMonthlyFinancialSummaryRepository _repository;

        public MonthlyFinancialSummaryService(IMonthlyFinancialSummaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MonthlyFinancialSummaryModel>> GetAllMonthlyFinancialSummariesAsync()
        {
            IEnumerable<MonthlyFinancialSummary> monthlyFinancialSummary = await _repository.GetAllAsync();

            return monthlyFinancialSummary?.Select(m => new MonthlyFinancialSummaryModel
            {
                Year = m.Year,
                Month = m.Month,
                TotalRevenue = m.TotalRevenue

            }) ?? Enumerable.Empty<MonthlyFinancialSummaryModel>();
        }

    }
}
