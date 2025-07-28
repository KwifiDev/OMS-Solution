using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class RolesSummaryRepository : GenericViewRepository<RolesSummary>, IRolesSummaryRepository
    {
        public RolesSummaryRepository(AppDbContext context) : base(context)
        {

        }
    }
}
