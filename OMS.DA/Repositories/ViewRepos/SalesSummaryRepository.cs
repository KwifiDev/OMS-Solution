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

    }
}
