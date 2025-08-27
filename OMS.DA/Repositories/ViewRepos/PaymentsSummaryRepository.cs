using Microsoft.EntityFrameworkCore;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class PaymentsSummaryRepository : GenericViewRepository<PaymentsSummary>, IPaymentsSummaryRepository
    {
        public PaymentsSummaryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<PaymentsSummary>> GetPaymentsByAccountIdPagedAsync(int accountId, PaginationParams parameters)
        {
            var items = await _dbSet
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
                         .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                         .Take(parameters.PageSize)
                         .ToListAsync();

            return new PagedResult<PaymentsSummary>
            {
                Items = items,
                TotalCount = _dbSet.Count(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            };
        }
    }
}
