using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class PaymentsSummaryRepository : GenericViewRepository<PaymentsSummary>, IPaymentsSummaryRepository
    {
        private readonly DbSet<PaymentsSummary> _paymentsSummaries;

        public PaymentsSummaryRepository(AppDbContext context) : base(context)
        {
            _paymentsSummaries = context.Set<PaymentsSummary>();
        }

    }
}
