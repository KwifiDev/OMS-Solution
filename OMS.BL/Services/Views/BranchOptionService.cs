using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class BranchOptionService : GenericViewService<BranchOption, BranchOptionDto>, IBranchOptionService
    {
        private readonly IBranchOptionRepository _branchOperationalMetricRepository;

        public BranchOptionService(IGenericViewRepository<BranchOption> genericRepo,
                                   IMapperService mapper,
                                   IBranchOptionRepository repository) : base(genericRepo, mapper)
        {
            _branchOperationalMetricRepository = repository;
        }
    }
}
