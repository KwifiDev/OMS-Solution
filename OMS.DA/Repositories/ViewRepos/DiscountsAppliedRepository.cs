using Microsoft.EntityFrameworkCore;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class DiscountsAppliedRepository : GenericViewRepository<DiscountsApplied>, IDiscountsAppliedRepository
    {
        public DiscountsAppliedRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<DiscountsApplied>> GetByServiceIdPagedAsync(int serviceId, PaginationParams parameters)
        {
            var items = await _dbSet
                         .AsNoTracking()
                         .Where(e => e.ServiceId == serviceId)
                         .Select(e => new DiscountsApplied
                         {
                             Id = e.Id,
                             ServiceName = e.ServiceName,
                             ServicePrice = e.ServicePrice,
                             ClientType = e.ClientType,
                             Discount = e.Discount,
                         })
                         .OrderByDescending(e => e.Id)
                         .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                         .Take(parameters.PageSize)
                         .ToListAsync();

            return new PagedResult<DiscountsApplied>
            {
                Items = items,
                TotalItems = await _dbSet.Where(e => e.ServiceId == serviceId).CountAsync(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            };
        }
    }
}
