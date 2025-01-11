using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IPaymentsSummaryService
    {
        Task<IEnumerable<PaymentsSummaryModel>> GetAllPaymentsSummaryAsync();
        Task<PaymentsSummaryModel?> GetPaymentSummaryByIdAsync(int paymentId);
    }
}
