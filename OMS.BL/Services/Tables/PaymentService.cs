using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PaymentModel>> GetAllPaymentsAsync()
        {
            IEnumerable<Payment> payments = await _repository.GetAllAsync();

            return payments?.Select(p => new PaymentModel
            {
                PaymentId = p.PaymentId,
                AccountId = p.AccountId,
                Amount = p.Amount,
                Notes = p.Notes,
                CreatedAt = p.CreatedAt,
                CreatedByUserId = p.CreatedByUserId

            }) ?? Enumerable.Empty<PaymentModel>();
        }

        public async Task<PaymentModel?> GetPaymentByIdAsync(int paymentId)
        {
            Payment? payment = await _repository.GetByIdAsync(paymentId);

            return payment == null ? null : new PaymentModel
            {
                PaymentId = payment.PaymentId,
                AccountId = payment.AccountId,
                Amount = payment.Amount,
                Notes = payment.Notes,
                CreatedAt = payment.CreatedAt,
                CreatedByUserId = payment.CreatedByUserId
            };
        }

    }
}
