using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchModel>> GetAllBranchesAsync();
        Task<BranchModel?> GetBranchByIdAsync(int branchId);
        Task<bool> AddBranchAsync(BranchModel model);
        Task<bool> UpdateBranchAsync(BranchModel model);
        Task<bool> DeleteBranchAsync(int branchId);
    }
}
