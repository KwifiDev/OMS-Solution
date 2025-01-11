using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentModel>> GetAllPaymentsAsync();
        Task<PaymentModel?> GetPaymentByIdAsync(int paymentId);
    }
}
