using Microsoft.EntityFrameworkCore;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class ClientsSummaryRepository : GenericViewRepository<ClientsSummary>, IClientsSummaryRepository
    {
        public ClientsSummaryRepository(AppDbContext context) : base(context)
        {
        }


        public override async Task<PagedResult<ClientsSummary>> GetPagedAsync(PaginationParams parameters)
        {
            var items = await _dbSet.AsNoTracking()
                                   .OrderByDescending(e => e.TotalDebts)
                                   .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                   .Take(parameters.PageSize)
                                   .ToListAsync();

            return new PagedResult<ClientsSummary>
            {
                Items = items,
                TotalItems = await _dbSet.CountAsync(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
            };
        }
    }
}
