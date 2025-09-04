using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class TransactionsByTypeRepository : ITransactionsByTypeRepository
    {
        private readonly DbSet<TransactionsByType> _dbSet;

        public TransactionsByTypeRepository(AppDbContext context)
        {
            _dbSet = context.Set<TransactionsByType>();
        }

        public async Task<IEnumerable<TransactionsByType>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
