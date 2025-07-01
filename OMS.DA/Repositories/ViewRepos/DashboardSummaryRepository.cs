using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class DashboardSummaryRepository : GenericViewRepository<DashboardSummary>, IDashboardSummaryRepository
    {
        public DashboardSummaryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
