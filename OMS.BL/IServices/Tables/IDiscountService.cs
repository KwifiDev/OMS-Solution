using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountModel>> GetAllDiscountsAsync();
        Task<DiscountModel?> GetDiscountByIdAsync(int discountId);
        Task<bool> AddDiscountAsync(DiscountModel model);
        Task<bool> UpdateDiscountAsync(DiscountModel model);
        Task<bool> DeleteDiscountAsync(int discountId);
    }
}
