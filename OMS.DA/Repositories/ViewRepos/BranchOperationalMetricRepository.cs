using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class BranchOperationalMetricRepository : GenericViewRepository<BranchOperationalMetric>, IBranchOperationalMetricRepository
    {
        private readonly DbSet<BranchOperationalMetric> _branchOperationalMetric;

        public BranchOperationalMetricRepository(AppDbContext context) : base(context)
        {
            _branchOperationalMetric = context.Set<BranchOperationalMetric>();
        }



        /*
                 public async Task<BranchOperationalMetric?> GetBranchOperationalMetricByIdAsync(int branchId)
        {
            return await _branchOperationalMetric
                        .AsNoTracking()
                        .Where(m => m.BranchId == branchId)
                        .SingleOrDefaultAsync();
        }
         */
    }
}
