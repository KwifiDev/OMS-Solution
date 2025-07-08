using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<DiscountsApplied>> GetByServiceIdAsync(int serviceId)
        {
            return await _dbSet
                         .AsNoTracking()
                         .Where(e => e.ServiceId == serviceId)
                         .Select(e => new DiscountsApplied
                         {
                             DiscountId = e.DiscountId,
                             ServiceName = e.ServiceName,
                             ServicePrice = e.ServicePrice,
                             ClientType = e.ClientType,
                             Discount = e.Discount,
                         })
                         .ToListAsync();
        }
    }
}
