using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
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

        public async Task<IEnumerable<BranchOperationalMetricModel>> GetAllBranchesOperationalMetricAsync()
        {
            IEnumerable<BranchOperationalMetric> branchOperationalMetric = await _repository.GetAllAsync();

            return branchOperationalMetric?.Select(b => new BranchOperationalMetricModel
            {
                BranchId = b.BranchId,
                Name = b.Name,
                Address = b.Address,
                TotalEmployees = b.TotalEmployees

            }) ?? Enumerable.Empty<BranchOperationalMetricModel>();
        }

        public async Task<BranchOperationalMetricModel?> GetBranchOperationalMetricByIdAsync(int branchId)
        {
            BranchOperationalMetric? branch = await _repository.GetBranchOperationalMetricByIdAsync(branchId);

            return branch == null ? null : new BranchOperationalMetricModel
            {
                BranchId = branch.BranchId,
                Name = branch.Name,
                Address = branch.Address,
                TotalEmployees = branch.TotalEmployees
            };
        }

    }
}
