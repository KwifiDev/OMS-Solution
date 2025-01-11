using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IBranchOperationalMetricService
    {
        Task<IEnumerable<BranchOperationalMetricModel>> GetAllBranchesOperationalMetricAsync();
        Task<BranchOperationalMetricModel?> GetBranchOperationalMetricByIdAsync(int branchId);
    }
}
