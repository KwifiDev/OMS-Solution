using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class DebtsByStatusRepository : GenericViewRepository<DebtsByStatus>, IDebtsByStatusRepository
    {
        public DebtsByStatusRepository(AppDbContext context) : base(context)
        {

        }
    }
}
