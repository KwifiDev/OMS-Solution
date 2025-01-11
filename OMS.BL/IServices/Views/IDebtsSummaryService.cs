using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IDebtsSummaryService
    {
        Task<IEnumerable<DebtsSummaryModel>> GetAllDebtsSummaryAsync();
        Task<DebtsSummaryModel?> GetDebtSummaryByIdAsync(int debtId);
    }
}
