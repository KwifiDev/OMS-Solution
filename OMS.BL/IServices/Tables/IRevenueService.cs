using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IRevenueService
    {
        Task<IEnumerable<RevenueModel>> GetAllRevenuesAsync();
        Task<RevenueModel?> GetRevenueByIdAsync(int revenueId);
        Task<bool> AddRevenueAsync(RevenueModel model);
        Task<bool> UpdateRevenueAsync(RevenueModel model);
        Task<bool> DeleteRevenueAsync(int revenueId);
    }
}
