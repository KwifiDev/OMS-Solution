using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class MonthlyFinancialSummaryRepository : IMonthlyFinancialSummaryRepository
    {
        private readonly DbSet<MonthlyFinancialSummary> _dbSet;

        public MonthlyFinancialSummaryRepository(AppDbContext context)
        {
            _dbSet = context.Set<MonthlyFinancialSummary>();
        }

        public async Task<IEnumerable<MonthlyFinancialSummary>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
