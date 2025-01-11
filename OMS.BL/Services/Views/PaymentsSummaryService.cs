using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class PaymentsSummaryService : IPaymentsSummaryService
    {
        private readonly IPaymentsSummaryRepository _repository;

        public PaymentsSummaryService(IPaymentsSummaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PaymentsSummaryModel>> GetAllPaymentsSummaryAsync()
        {
            IEnumerable<PaymentsSummary> paymentsSummary = await _repository.GetAllAsync();

            return paymentsSummary?.Select(p => new PaymentsSummaryModel
            {
                PaymentId = p.PaymentId,
                ClientName = p.ClientName,
                AmountPaid = p.AmountPaid,
                CreatedAt = p.CreatedAt,
                Notes = p.Notes

            }) ?? Enumerable.Empty<PaymentsSummaryModel>();
        }

        public async Task<PaymentsSummaryModel?> GetPaymentSummaryByIdAsync(int paymentId)
        {
            PaymentsSummary? payment = await _repository.GetPaymentSummaryByIdAsync(paymentId);

            return payment == null ? null : new PaymentsSummaryModel
            {
                PaymentId = payment.PaymentId,
                ClientName = payment.ClientName,
                AmountPaid = payment.AmountPaid,
                CreatedAt = payment.CreatedAt,
                Notes = payment.Notes
            };
        }

    }
}
