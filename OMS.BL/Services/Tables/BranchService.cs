using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class BranchService : GenericService<Branch, BranchDto>, IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IGenericRepository<Branch> repo,
                             IMapperService mapper,
                             IBranchRepository branchRepository) : base(repo, mapper)
        {
            _branchRepository = branchRepository;
        }



        /*
         public async Task<IEnumerable<BranchDto>> GetAllBranchesAsync()
        {
            IEnumerable<Branch> branches = await _repository.GetAllAsync();

            return branches?.Select(b => new BranchDto
            {
                BranchId = b.BranchId,
                Name = b.Name,
                Address = b.Address

            }) ?? Enumerable.Empty<BranchDto>();
        }

        public async Task<BranchDto?> GetBranchByIdAsync(int branchId)
        {
            Branch? branch = await _repository.GetByIdAsync(branchId);

            return branch == null ? null : new BranchDto
            {
                BranchId = branch.BranchId,
                Name = branch.Name,
                Address = branch.Address
            };
        }

        public async Task<bool> AddBranchAsync(BranchDto dto)
        {
            if (dto == null) return false;

            Branch branch = new Branch
            {
                BranchId = dto.BranchId,
                Name = dto.Name,
                Address = dto.Address
            };

            bool success = await _repository.AddAsync(branch);

            if (success) dto.BranchId = branch.BranchId;

            return success;
        }

        public async Task<bool> UpdateBranchAsync(BranchDto dto)
        {
            if (dto == null) return false;

            Branch? branch = await _repository.GetByIdAsync(dto.BranchId);

            if (branch == null) return false;

            branch.Name = dto.Name;
            branch.Address = dto.Address;

            return await _repository.UpdateAsync(branch);
        }

        public async Task<bool> DeleteBranchAsync(int branchId)
        {
            if (branchId <= 0) return false;

            return await _repository.DeleteAsync(branchId);
        }
         */

    }
}
