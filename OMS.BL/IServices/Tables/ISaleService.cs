using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleModel>> GetAllSalesAsync();
        Task<SaleModel?> GetSaleByIdAsync(int saleId);
        Task<bool> AddSaleAsync(SaleModel model);
        Task<bool> UpdateSaleAsync(SaleModel model);
        Task<bool> DeleteSaleAsync(int saleId);
    }
}
