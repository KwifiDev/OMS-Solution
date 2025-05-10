using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.EntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PaymentService : GenericService<Payment, PaymentModel>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IGenericRepository<Payment> genericRepo,
                              IMapperService mapper,
                              IPaymentRepository repository) : base(genericRepo, mapper)
        {
            _paymentRepository = repository;
        }

        public override Task<bool> AddAsync(PaymentModel model)
            => throw new NotSupportedException("Add operation is not supported for PaymentService.");
        public override Task<bool> UpdateAsync(PaymentModel model)
            => throw new NotSupportedException("Update operation is not supported for PaymentService.");
        public override Task<bool> DeleteAsync(int id)
            => throw new NotSupportedException("Delete operation is not supported for PaymentService.");

    }
}
