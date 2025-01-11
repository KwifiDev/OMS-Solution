using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class SalesSummaryRepository : GenericViewRepository<SalesSummary>, IGenericViewRepository<SalesSummary>, ISalesSummaryRepository
    {
        private readonly DbSet<SalesSummary> _salesSummaries;

        public SalesSummaryRepository(AppDbContext context) : base(context)
        {
            _salesSummaries = context.Set<SalesSummary>();
        }

        public async Task<SalesSummary?> GetSaleSummaryByIdAsync(int saleId)
        {
            return await _salesSummaries
                        .AsNoTracking()
                        .Where(m => m.SaleId == saleId)
                        .SingleOrDefaultAsync();
        }
    }
}
