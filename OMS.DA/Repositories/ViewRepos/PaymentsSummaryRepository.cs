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

    }
}
