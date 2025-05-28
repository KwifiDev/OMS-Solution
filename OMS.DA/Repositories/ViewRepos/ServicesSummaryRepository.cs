using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class ServicesSummaryRepository : GenericViewRepository<ServicesSummary>, IServicesSummaryRepository
    {
        private readonly DbSet<ServicesSummary> _servicesSummary;

        public ServicesSummaryRepository(AppDbContext context) : base(context)
        {
            _servicesSummary = context.Set<ServicesSummary>();
        }
    }
}
