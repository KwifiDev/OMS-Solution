using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class TransactionsSummaryService : ITransactionsSummaryService
    {
        private readonly ITransactionsSummaryRepository _repository;

        public TransactionsSummaryService(ITransactionsSummaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TransactionsSummaryModel>> GetAllTransactionsSummaryAsync()
        {
            IEnumerable<TransactionsSummary> transactionsSummary = await _repository.GetAllAsync();

            return transactionsSummary?.Select(t => new TransactionsSummaryModel
            {
               TransactionId = t.TransactionId,
               UserAccount = t.UserAccount,
               TransactionType = t.TransactionType,
               Amount = t.Amount,
               CreatedAt = t.CreatedAt,
               Notes = t.Notes

            }) ?? Enumerable.Empty<TransactionsSummaryModel>();
        }

        public async Task<TransactionsSummaryModel?> GetTransactionSummaryByIdAsync(int transactionId)
        {
            TransactionsSummary? transaction = await _repository.GetTransactionSummaryByIdAsync(transactionId);

            return transaction == null ? null : new TransactionsSummaryModel
            {
                TransactionId = transaction.TransactionId,
                UserAccount = transaction.UserAccount,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                Notes = transaction.Notes
            };
        }

    }
}
