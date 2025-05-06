using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class TransactionService : GenericService<Transaction, TransactionModel>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IGenericRepository<Transaction> genericRepo,
                                  IMapperService mapper,
                                  ITransactionRepository repository) : base(genericRepo, mapper)
        {
            _transactionRepository = repository;
        }

        public override Task<bool> AddAsync(TransactionModel dto)
           => throw new NotSupportedException("Add operation is not supported for TransactionService.");
        public override Task<bool> UpdateAsync(TransactionModel dto)
            => throw new NotSupportedException("Update operation is not supported for TransactionService.");
        public override Task<bool> DeleteAsync(int id)
            => throw new NotSupportedException("Delete operation is not supported for TransactionService.");

    }
}
