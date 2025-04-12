using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        private readonly DbSet<Branch> _branches;

        public BranchRepository(AppDbContext context) : base(context)
        {
            _branches = context.Set<Branch>();
        }

        public async Task<List<Branch>> GetAllBranchesOption()
        {
            return await _branches.Select(b => new Branch { BranchId = b.BranchId, Name = b.Name }).ToListAsync();
        }

    }
}
