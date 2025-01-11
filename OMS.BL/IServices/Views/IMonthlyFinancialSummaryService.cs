using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IMonthlyFinancialSummaryService
    {
        Task<IEnumerable<MonthlyFinancialSummaryModel>> GetAllMonthlyFinancialSummariesAsync();
    }
}
