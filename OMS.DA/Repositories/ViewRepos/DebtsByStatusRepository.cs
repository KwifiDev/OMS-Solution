using OMS.DA.Context;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class DebtsByStatusRepository : GenericViewRepository<DebtsByStatus>, IGenericViewRepository<DebtsByStatus>
    {
        public DebtsByStatusRepository(AppDbContext context) : base(context)
        {

        }
    }
}
