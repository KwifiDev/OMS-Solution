using OMS.BL.IServices.Tables;
using OMS.BL.Dtos.Tables;
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

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            IEnumerable<Payment> payments = await _repository.GetAllAsync();

            return payments?.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                AccountId = p.AccountId,
                Amount = p.Amount,
                Notes = p.Notes,
                CreatedAt = p.CreatedAt,
                CreatedByUserId = p.CreatedByUserId

            }) ?? Enumerable.Empty<PaymentDto>();
        }

        public async Task<PaymentDto?> GetPaymentByIdAsync(int paymentId)
        {
            Payment? payment = await _repository.GetByIdAsync(paymentId);

            return payment == null ? null : new PaymentDto
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
