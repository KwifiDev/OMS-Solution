using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class BranchOperationalMetricService : GenericViewService<BranchOperationalMetric, BranchOperationalMetricDto>, IBranchOperationalMetricService
    {
        private readonly IBranchOperationalMetricRepository _branchOperationalMetricRepository;

        public BranchOperationalMetricService(IGenericViewRepository<BranchOperationalMetric> genericRepo,
                                              IMapperService mapper,
                                              IBranchOperationalMetricRepository repository) : base(genericRepo, mapper)
        {
            _branchOperationalMetricRepository = repository;
        }

        /*
                 public async Task<IEnumerable<BranchOperationalMetricDto>> GetAllBranchesOperationalMetricAsync()
        {
            IEnumerable<BranchOperationalMetric> branchOperationalMetric = await _repository.GetAllAsync();

            return branchOperationalMetric?.Select(b => new BranchOperationalMetricDto
            {
                BranchId = b.BranchId,
                Name = b.Name,
                Address = b.Address,
                TotalEmployees = b.TotalEmployees

            }) ?? Enumerable.Empty<BranchOperationalMetricDto>();
        }

        public async Task<BranchOperationalMetricDto?> GetBranchOperationalMetricByIdAsync(int branchId)
        {
            BranchOperationalMetric? branch = await _repository.GetByIdAsync(branchId);

            return branch == null ? null : new BranchOperationalMetricDto
            {
                BranchId = branch.BranchId,
                Name = branch.Name,
                Address = branch.Address,
                TotalEmployees = branch.TotalEmployees
            };
        }
         */

    }
}
