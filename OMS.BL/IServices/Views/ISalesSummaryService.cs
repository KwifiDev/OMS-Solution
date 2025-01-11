using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface ISalesSummaryService
    {
        Task<IEnumerable<SalesSummaryModel>> GetAllSalesSummaryAsync();
        Task<SalesSummaryModel?> GetSaleSummaryByIdAsync(int saleId);
    }
}
