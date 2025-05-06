using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class BranchOperationalMetricService : GenericViewService<BranchOperationalMetric, BranchOperationalMetricModel>, IBranchOperationalMetricService
    {
        private readonly IBranchOperationalMetricRepository _branchOperationalMetricRepository;

        public BranchOperationalMetricService(IGenericViewRepository<BranchOperationalMetric> genericRepo,
                                              IMapperService mapper,
                                              IBranchOperationalMetricRepository repository) : base(genericRepo, mapper)
        {
            _branchOperationalMetricRepository = repository;
        }

    }
}
