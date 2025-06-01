using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
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

        public async Task<IEnumerable<SalesSummaryModel>> GetByClientIdAsync(int clientId)
        {
            var salesSummary = await _salesSummaryRepository.GetByClientIdAsync(clientId);

            return _mapper.Map<IEnumerable<SalesSummary>, IEnumerable<SalesSummaryModel>>(salesSummary);
        }

    }
}
