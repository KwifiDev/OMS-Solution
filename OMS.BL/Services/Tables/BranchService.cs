using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repository;

        public BranchService(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BranchModel>> GetAllBranchesAsync()
        {
            IEnumerable<Branch> branches = await _repository.GetAllAsync();

            return branches?.Select(b => new BranchModel
            {
                BranchId = b.BranchId,
                Name = b.Name,
                Address = b.Address

            }) ?? Enumerable.Empty<BranchModel>();
        }

        public async Task<BranchModel?> GetBranchByIdAsync(int branchId)
        {
            Branch? branch = await _repository.GetByIdAsync(branchId);

            return branch == null ? null : new BranchModel
            {
                BranchId = branch.BranchId,
                Name = branch.Name,
                Address = branch.Address
            };
        }

        public async Task<bool> AddBranchAsync(BranchModel model)
        {
            if (model == null) return false;

            Branch branch = new Branch
            {
                BranchId = model.BranchId,
                Name = model.Name,
                Address = model.Address
            };

            bool success = await _repository.AddAsync(branch);

            if (success) model.BranchId = branch.BranchId;

            return success;
        }

        public async Task<bool> UpdateBranchAsync(BranchModel model)
        {
            if (model == null) return false;

            Branch? branch = await _repository.GetByIdAsync(model.BranchId);

            if (branch == null) return false;

            branch.Name = model.Name;
            branch.Address = model.Address;

            return await _repository.UpdateAsync(branch);
        }

        public async Task<bool> DeleteBranchAsync(int branchId)
        {
            if (branchId <= 0) return false;

            return await _repository.DeleteAsync(branchId);
        }
    }
}
