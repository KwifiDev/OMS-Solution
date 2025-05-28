using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class DiscountsAppliedRepository : GenericViewRepository<DiscountsApplied>, IDiscountsAppliedRepository
    {
        private readonly DbSet<DiscountsApplied> _discountsApplieds;

        public DiscountsAppliedRepository(AppDbContext context) : base(context)
        {
            _discountsApplieds = context.Set<DiscountsApplied>();
        }

        public async Task<IEnumerable<DiscountsApplied>> GetByServiceIdAsync(int serviceId)
        {
            return await _discountsApplieds
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
