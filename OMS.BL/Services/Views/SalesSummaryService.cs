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

    }
}
