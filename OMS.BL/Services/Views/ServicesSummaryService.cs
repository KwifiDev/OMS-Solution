using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ServicesSummaryService : GenericViewService<ServicesSummary, ServicesSummaryModel>, IServicesSummaryService
    {
        private readonly IServicesSummaryRepository _servicesSummaryRepository;

        public ServicesSummaryService(IGenericViewRepository<ServicesSummary> genericRepo,
                                      IMapperService mapper,
                                      IServicesSummaryRepository repository) : base(genericRepo, mapper)
        {
            _servicesSummaryRepository = repository;
        }
    }
}
