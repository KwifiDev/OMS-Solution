using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<DebtsSummary>> GetByClientIdAsync(int clientId)
        {
            return await _dbSet
                         .AsNoTracking()
                         .Where(e => e.ClientId == clientId)
                         .Select(e => new DebtsSummary
                         {
                             DebtId = e.DebtId,
                             ServiceName = e.ServiceName,
                             Description = e.Description,
                             Notes = e.Notes,
                             TotalDebts = e.TotalDebts,
                             Status = e.Status
                         })
                         .ToListAsync();
        }
    }
}
