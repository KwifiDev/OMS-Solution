using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IDiscountsAppliedService
    {
        Task<IEnumerable<DiscountsAppliedModel>> GetAllDiscountsAppliedAsync();
        Task<DiscountsAppliedModel?> GetDiscountAppliedByIdAsync(int discountId);
    }
}
