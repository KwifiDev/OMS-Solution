using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class TransactionsSummaryRepository : GenericViewRepository<TransactionsSummary>, ITransactionsSummaryRepository
    {
        private readonly DbSet<TransactionsSummary> _transactionsSummaries;

        public TransactionsSummaryRepository(AppDbContext context) : base(context)
        {
            _transactionsSummaries = context.Set<TransactionsSummary>();
        }


        public async Task<IEnumerable<TransactionsSummary>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _transactionsSummaries
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
