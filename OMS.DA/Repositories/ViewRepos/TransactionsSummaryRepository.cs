using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class TransactionsSummaryRepository : GenericViewRepository<TransactionsSummary>, ITransactionsSummaryRepository
    {
        public TransactionsSummaryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TransactionsSummary>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _dbSet
                         .AsNoTracking()
                         .Where(e => e.AccountId == accountId)
                         .Select(e => new TransactionsSummary
                         {
                             TransactionId = e.TransactionId,
                             TransactionType = e.TransactionType,
                             Amount = e.Amount,
                             CreatedAt = e.CreatedAt,
                             Notes = e.Notes
                         }).OrderByDescending(e => e.CreatedAt)
                         .ToListAsync();
        }

    }
}
