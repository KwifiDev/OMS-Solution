using Microsoft.EntityFrameworkCore;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class DebtsSummaryRepository : GenericViewRepository<DebtsSummary>, IDebtsSummaryRepository
    {
        public DebtsSummaryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<DebtsSummary>> GetByClientIdPagedAsync(int clientId, PaginationParams parameters)
        {
            var items = await _dbSet
                         .AsNoTracking()
                         .Where(e => e.ClientId == clientId)
                         .Select(e => new DebtsSummary
                         {
                             Id = e.Id,
                             ServiceName = e.ServiceName,
                             Description = e.Description,
                             Notes = e.Notes,
                             TotalDebts = e.TotalDebts,
                             Status = e.Status
                         })
                         .OrderByDescending(e => e.Status == "غير مدفوع")
                         .ThenByDescending(e => e.Id)
                         .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                         .Take(parameters.PageSize)
                         .ToListAsync();

            return new PagedResult<DebtsSummary>
            {
                Items = items,
                TotalItems = await _dbSet.Where(e => e.ClientId == clientId).CountAsync(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            };
        }
    }
}
