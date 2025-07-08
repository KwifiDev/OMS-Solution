using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<SalesSummary>> GetByClientIdAsync(int clientId)
        {
            return await _dbSet
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
