using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class DiscountsAppliedRepository : GenericViewRepository<DiscountsApplied>, IGenericViewRepository<DiscountsApplied>, IDiscountsAppliedRepository
    {
        private readonly DbSet<DiscountsApplied> _discountsApplieds;

        public DiscountsAppliedRepository(AppDbContext context) : base(context)
        {
            _discountsApplieds = context.Set<DiscountsApplied>();
        }

        public async Task<DiscountsApplied?> GetDiscountAppliedByIdAsync(int discountId)
        {
            return await _discountsApplieds
                        .AsNoTracking()
                        .Where(m => m.DiscountId == discountId)
                        .SingleOrDefaultAsync();
        }
    }
}
