using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class TransactionsSummaryRepository : GenericViewRepository<TransactionsSummary>, IGenericViewRepository<TransactionsSummary>, ITransactionsSummaryRepository
    {
        private readonly DbSet<TransactionsSummary> _transactionsSummaries;

        public TransactionsSummaryRepository(AppDbContext context) : base(context)
        {
            _transactionsSummaries = context.Set<TransactionsSummary>();
        }

        public async Task<TransactionsSummary?> GetTransactionSummaryByIdAsync(int transactionId)
        {
            return await _transactionsSummaries
                         .AsNoTracking()
                         .Where(m => m.TransactionId == transactionId)
                         .SingleOrDefaultAsync();
        }
    }
}
