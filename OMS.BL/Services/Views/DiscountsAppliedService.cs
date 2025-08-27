using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DiscountsAppliedService : GenericViewService<DiscountsApplied, DiscountsAppliedModel>, IDiscountsAppliedService
    {
        private readonly IDiscountsAppliedRepository _discountsAppliedRepository;

        public DiscountsAppliedService(IGenericViewRepository<DiscountsApplied> genericRepo,
                                       IMapperService mapper,
                                       IDiscountsAppliedRepository repository) : base(genericRepo, mapper)
        {
            _discountsAppliedRepository = repository;
        }

        public async Task<PagedResult<DiscountsAppliedModel>> GetByServiceIdPagedAsync(int serviceId, PaginationParams parameters)
        {
            var pagedResult = await _discountsAppliedRepository.GetByServiceIdPagedAsync(serviceId, parameters);

            return new PagedResult<DiscountsAppliedModel>
            {
                Items = _mapper.Map<List<DiscountsApplied>, List<DiscountsAppliedModel>>(pagedResult.Items),
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }
    }
}
