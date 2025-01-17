using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class PaymentsSummaryService : GenericViewService<PaymentsSummary, PaymentsSummaryDto>, IPaymentsSummaryService
    {
        private readonly IPaymentsSummaryRepository _paymentsSummaryRepository;

        public PaymentsSummaryService(IGenericViewRepository<PaymentsSummary> genericRepo,
                                      IMapperService mapper,
                                      IPaymentsSummaryRepository repository) : base(genericRepo, mapper)
        {
            _paymentsSummaryRepository = repository;
        }


        /*
                 public async Task<IEnumerable<PaymentsSummaryDto>> GetAllPaymentsSummaryAsync()
        {
            IEnumerable<PaymentsSummary> paymentsSummary = await _paymentsSummaryRepository.GetAllAsync();

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
            PaymentsSummary? payment = await _paymentsSummaryRepository.GetByIdAsync(paymentId);

            return payment == null ? null : new PaymentsSummaryDto
            {
                PaymentId = payment.PaymentId,
                ClientName = payment.ClientName,
                AmountPaid = payment.AmountPaid,
                CreatedAt = payment.CreatedAt,
                Notes = payment.Notes
            };
        }

         */

    }
}
