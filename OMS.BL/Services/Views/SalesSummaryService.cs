using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class SalesSummaryService : GenericViewService<SalesSummary, SalesSummaryModel>, ISalesSummaryService
    {
        private readonly ISalesSummaryRepository _salesSummaryRepository;

        public SalesSummaryService(IGenericViewRepository<SalesSummary> genericRepo,
                                   IMapperService mapper,
                                   ISalesSummaryRepository repository) : base(genericRepo, mapper)
        {
            _salesSummaryRepository = repository;
        }

        public async Task<PagedResult<SalesSummaryModel>> GetByClientIdPagedAsync(int clientId, PaginationParams parameters)
        {
            var pagedResult = await _salesSummaryRepository.GetByClientIdPagedAsync(clientId, parameters);

            return new PagedResult<SalesSummaryModel>
            {
                Items = _mapper.Map<List<SalesSummary>, List<SalesSummaryModel>>(pagedResult.Items),
                TotalItems = pagedResult.TotalItems,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }

    }
}
