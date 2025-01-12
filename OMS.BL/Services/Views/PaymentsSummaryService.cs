using OMS.BL.IServices.Views;
using OMS.BL.Dtos.Views;
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

        public async Task<IEnumerable<PaymentsSummaryDto>> GetAllPaymentsSummaryAsync()
        {
            IEnumerable<PaymentsSummary> paymentsSummary = await _repository.GetAllAsync();

            return paymentsSummary?.Select(p => new PaymentsSummaryDto
            {
                PaymentId = p.PaymentId,
                ClientName = p.ClientName,
                AmountPaid = p.AmountPaid,
                CreatedAt = p.CreatedAt,
                Notes = p.Notes

            }) ?? Enumerable.Empty<PaymentsSummaryDto>();
        }

        public async Task<PaymentsSummaryDto?> GetPaymentSummaryByIdAsync(int paymentId)
        {
            PaymentsSummary? payment = await _repository.GetPaymentSummaryByIdAsync(paymentId);

            return payment == null ? null : new PaymentsSummaryDto
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
