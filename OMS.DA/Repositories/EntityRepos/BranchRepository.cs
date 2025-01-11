using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Context;
using OMS.DA.Entities;

namespace OMS.DA.Repositories.EntityRepos
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(AppDbContext context) : base(context)
        {

        }
    }
}
