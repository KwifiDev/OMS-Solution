using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class BranchOptionRepository : GenericViewRepository<BranchOption>, IBranchOptionRepository
    {
        private readonly DbSet<BranchOption> _branchOption;

        public BranchOptionRepository(AppDbContext context) : base(context)
        {
            _branchOption = context.Set<BranchOption>();
        }

    }
}
