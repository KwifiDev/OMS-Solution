using OMS.BL.Dtos.Tables;
using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class BranchService : GenericService<Branch, BranchDto>, IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IGenericRepository<Branch> genericRepo,
                             IMapperService mapper,
                             IBranchRepository branchRepository) : base(genericRepo, mapper)
        {
            _branchRepository = branchRepository;
        }

        public async Task<IEnumerable<BranchOptionDto>?> GetAllBranchesOption()
        {
            var branches = await _branchRepository.GetAllBranchesOption();

            return branches == null ? null : _mapperService.Map<Branch, BranchOptionDto>(branches);
        }

    }
}
