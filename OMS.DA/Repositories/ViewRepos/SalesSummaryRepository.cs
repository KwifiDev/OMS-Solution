using Microsoft.EntityFrameworkCore;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class SalesSummaryRepository : GenericViewRepository<SalesSummary>, ISalesSummaryRepository
    {
        public SalesSummaryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<SalesSummary>> GetByClientIdPagedAsync(int clientId, PaginationParams parameters)
        {
            var items = await _dbSet
                         .AsNoTracking()
                         .Where(e => e.ClientId == clientId)
                         .Select(e => new SalesSummary
                         {
                             SaleId = e.SaleId,
                             ServiceName = e.ServiceName,
                             Description = e.Description,
                             Notes = e.Notes,
                             TotalSales = e.TotalSales,
                             Status = e.Status
                         })
                         .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                         .Take(parameters.PageSize)
                         .ToListAsync();

            return new PagedResult<SalesSummary>
            {
                Items = items,
                TotalCount = _dbSet.Count(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            };
        }

    }
}
