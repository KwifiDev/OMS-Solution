using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.EntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PaymentService : GenericService<Payment, PaymentDto>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IGenericRepository<Payment> genericRepo,
                              IMapperService mapper,
                              IPaymentRepository repository) : base(genericRepo, mapper)
        {
            _paymentRepository = repository;
        }

        public override Task<bool> AddAsync(PaymentDto dto)
            => throw new NotSupportedException("Add operation is not supported for PaymentService.");
        public override Task<bool> UpdateAsync(PaymentDto dto)
            => throw new NotSupportedException("Update operation is not supported for PaymentService.");
        public override Task<bool> DeleteAsync(int id)
            => throw new NotSupportedException("Delete operation is not supported for PaymentService.");

        /*
          public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            IEnumerable<Payment> payments = await _paymentRepository.GetAllAsync();

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
            Payment? payment = await _paymentRepository.GetByIdAsync(paymentId);

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
         */

    }
}
