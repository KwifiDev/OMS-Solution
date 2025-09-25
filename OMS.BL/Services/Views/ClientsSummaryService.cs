using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ClientsSummaryService : GenericViewService<ClientsSummary, ClientsSummaryModel>, IClientsSummaryService
    {
        private readonly IClientsSummaryRepository _clientsSummaryRepository;

        public ClientsSummaryService(IGenericViewRepository<ClientsSummary> genericRepo,
                                     IMapperService mapper,
                                     IClientsSummaryRepository repository) : base(genericRepo, mapper)
        {
            _clientsSummaryRepository = repository;
        }

        public override async Task<PagedResult<ClientsSummaryModel>> GetPagedAsync(PaginationParams parameters)
        {
            var pagedResult = await _clientsSummaryRepository.GetPagedAsync(parameters);

            if (pagedResult.TotalItems == 0) return new PagedResult<ClientsSummaryModel>();

            return new PagedResult<ClientsSummaryModel>
            {
                Items = _mapper.Map<List<ClientsSummary>, List<ClientsSummaryModel>>(pagedResult.Items),
                TotalItems = pagedResult.TotalItems,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }
    }
}
