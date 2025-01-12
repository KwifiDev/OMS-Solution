using OMS.BL.IServices.Tables;
using OMS.BL.Dtos.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
        {
            IEnumerable<Transaction> transactions = await _repository.GetAllAsync();

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
            Transaction? transaction = await _repository.GetByIdAsync(transactionId);

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

    }
}
