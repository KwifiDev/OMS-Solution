using OMS.BL.IServices.Views;
using OMS.BL.Dtos.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class TransactionsByTypeService : ITransactionsByTypeService
    {
        private readonly ITransactionsByTypeRepository _repository;

        public TransactionsByTypeService(ITransactionsByTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TransactionsByTypeDto>> GetAllTransactionsByTypeAsync()
        {
            IEnumerable<TransactionsByType> transactionsByType = await _repository.GetAllAsync();

            return transactionsByType?.Select(t => new TransactionsByTypeDto
            {
               TransactionType = t.TransactionType,
               TotalTransactions = t.TotalTransactions,
               TotalAmount = t.TotalAmount

            }) ?? Enumerable.Empty<TransactionsByTypeDto>();
        }

    }
}
