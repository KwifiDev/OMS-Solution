using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class ClientsSummaryRepository : GenericViewRepository<ClientsSummary>, IClientsSummaryRepository
    {
        public ClientsSummaryRepository(AppDbContext context) : base(context)
        {
        }

    }
}
