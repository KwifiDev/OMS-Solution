using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class TransactionService : GenericService<Transaction, TransactionDto>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IGenericRepository<Transaction> genericRepo,
                                  IMapperService mapper,
                                  ITransactionRepository repository) : base(genericRepo, mapper)
        {
            _transactionRepository = repository;
        }

        public override Task<bool> AddAsync(TransactionDto dto)
           => throw new NotSupportedException("Add operation is not supported for TransactionService.");
        public override Task<bool> UpdateAsync(TransactionDto dto)
            => throw new NotSupportedException("Update operation is not supported for TransactionService.");
        public override Task<bool> DeleteAsync(int id)
            => throw new NotSupportedException("Delete operation is not supported for TransactionService.");

        /*
         
        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
        {
            IEnumerable<Transaction> transactions = await _transactionRepository.GetAllAsync();

            return transactions?.Select(t => new TransactionDto
            {
                TransactionId = t.TransactionId,
                AccountId = t.AccountId,
                TransactionType = t.TransactionType,
                Amount = t.Amount,
                Notes = t.Notes,
                CreatedAt = t.CreatedAt,
                CreatedByUserId = t.CreatedByUserId

            }) ?? Enumerable.Empty<TransactionDto>();
        }

        public async Task<TransactionDto?> GetTransactionByIdAsync(int transactionId)
        {
            Transaction? transaction = await _transactionRepository.GetByIdAsync(transactionId);

            return transaction == null ? null : new TransactionDto
            {
                TransactionId = transaction.TransactionId,
                AccountId = transaction.AccountId,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                Notes = transaction.Notes,
                CreatedAt = transaction.CreatedAt,
                CreatedByUserId = transaction.CreatedByUserId
            };
        }
         */

    }
}
