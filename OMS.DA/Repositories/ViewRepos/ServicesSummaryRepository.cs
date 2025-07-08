using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class ServicesSummaryRepository : GenericViewRepository<ServicesSummary>, IServicesSummaryRepository
    {
        public ServicesSummaryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
