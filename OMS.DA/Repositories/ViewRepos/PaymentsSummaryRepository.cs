using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class PaymentsSummaryRepository : GenericViewRepository<PaymentsSummary>, IPaymentsSummaryRepository
    {
        private readonly DbSet<PaymentsSummary> _paymentsSummaries;

        public PaymentsSummaryRepository(AppDbContext context) : base(context)
        {
            _paymentsSummaries = context.Set<PaymentsSummary>();
        }

        public async Task<IEnumerable<PaymentsSummary>> GetPaymentsByAccountIdAsync(int accountId)
        {
            return await _paymentsSummaries
                         .AsNoTracking()
                         .Where(e => e.AccountId == accountId)
                         .Select(e => new PaymentsSummary
                         {
                             PaymentId = e.PaymentId,
                             AmountPaid = e.AmountPaid,
                             CreatedAt = e.CreatedAt,
                             Notes = e.Notes,
                             EmployeeName = e.EmployeeName

                         }).OrderByDescending(e => e.CreatedAt)
                         .ToListAsync();
        }
    }
}
