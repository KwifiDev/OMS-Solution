using Microsoft.EntityFrameworkCore;
using OMS.Common.Extensions.Pagination;
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

        public async Task<PagedResult<TransactionsSummary>> GetTransactionsByAccountIdPagedAsync(int accountId, PaginationParams parameters)
        {
            var items = await _dbSet
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
                         .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                         .Take(parameters.PageSize)
                         .ToListAsync();

            return new PagedResult<TransactionsSummary>
            {
                Items = items,
                TotalItems = _dbSet.Count(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            };
        }

    }
}
