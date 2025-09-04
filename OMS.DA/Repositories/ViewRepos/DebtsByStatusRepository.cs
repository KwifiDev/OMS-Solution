using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class DebtsByStatusRepository : IDebtsByStatusRepository
    {
        private readonly DbSet<DebtsByStatus> _dbSet;

        public DebtsByStatusRepository(AppDbContext context)
        {
            _dbSet = context.Set<DebtsByStatus>();
        }

        public async Task<IEnumerable<DebtsByStatus>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
