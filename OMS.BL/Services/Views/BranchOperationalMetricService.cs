using OMS.BL.IServices.Views;
using OMS.BL.Dtos.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class BranchOperationalMetricService : IBranchOperationalMetricService
    {
        private readonly IBranchOperationalMetricRepository _repository;

        public BranchOperationalMetricService(IBranchOperationalMetricRepository repository)
        {
            _repository = repository;
        }

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

    }
}
