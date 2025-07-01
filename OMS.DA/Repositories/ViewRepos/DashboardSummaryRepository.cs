using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class DashboardSummaryRepository : IDashboardSummaryRepository
    {
        private readonly DbSet<DashboardSummary> _dbSet;

        public DashboardSummaryRepository(AppDbContext context)
        {
            _dbSet = context.Set<DashboardSummary>();
        }

        public async Task<DashboardSummary?> GetData()
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync();
        }
    }
}
