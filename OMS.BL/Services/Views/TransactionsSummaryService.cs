using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;
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

        public async Task<PagedResult<TransactionsSummaryModel>> GetTransactionsByAccountIdPagedAsync(int accountId, PaginationParams parameters)
        {
            var pagedResult = await _transactionsSummaryRepository.GetTransactionsByAccountIdPagedAsync(accountId, parameters);

            return new PagedResult<TransactionsSummaryModel>
            {
                Items = _mapper.Map<List<TransactionsSummary>, List<TransactionsSummaryModel>>(pagedResult.Items),
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }
    }
}
