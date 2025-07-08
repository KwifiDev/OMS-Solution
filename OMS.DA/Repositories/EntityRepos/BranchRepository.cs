using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Branch>> GetAllBranchesOption()
        {
            return await _dbSet.Select(b => new Branch { BranchId = b.BranchId, Name = b.Name }).ToListAsync();
        }

    }
}
