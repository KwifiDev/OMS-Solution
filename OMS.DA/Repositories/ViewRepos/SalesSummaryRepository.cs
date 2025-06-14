using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class SalesSummaryRepository : GenericViewRepository<SalesSummary>, ISalesSummaryRepository
    {
        private readonly DbSet<SalesSummary> _salesSummaries;

        public SalesSummaryRepository(AppDbContext context) : base(context)
        {
            _salesSummaries = context.Set<SalesSummary>();
        }

        public async Task<IEnumerable<SalesSummary>> GetByClientIdAsync(int clientId)
        {
            return await _salesSummaries
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
                         .ToListAsync();
        }

    }
}
