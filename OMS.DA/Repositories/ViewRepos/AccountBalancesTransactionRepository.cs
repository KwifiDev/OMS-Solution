using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class AccountBalancesTransactionRepository : GenericViewRepository<AccountBalancesTransaction>, IAccountBalancesTransactionRepository
    {
        public AccountBalancesTransactionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
