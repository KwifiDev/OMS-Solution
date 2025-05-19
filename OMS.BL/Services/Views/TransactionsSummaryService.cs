using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class TransactionsSummaryService : GenericViewService<TransactionsSummary, TransactionsSummaryModel>, ITransactionsSummaryService
    {
        private readonly ITransactionsSummaryRepository _transactionsSummaryRepository;

        public TransactionsSummaryService(IGenericViewRepository<TransactionsSummary> genericRepo,
                                          IMapperService mapper,
                                          ITransactionsSummaryRepository repository) : base(genericRepo, mapper)
        {
            _transactionsSummaryRepository = repository;
        }

        public async Task<IEnumerable<TransactionsSummaryModel>> GetTransactionsByAccountIdAsync(int accountId)
        {
            var transactionsSummary = await _transactionsSummaryRepository.GetTransactionsByAccountIdAsync(accountId);

            return _mapper.Map<IEnumerable<TransactionsSummary>, IEnumerable<TransactionsSummaryModel>>(transactionsSummary);
        }
    }
}
