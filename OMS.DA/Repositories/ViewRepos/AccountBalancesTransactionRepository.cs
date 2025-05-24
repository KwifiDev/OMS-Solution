using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class AccountBalancesTransactionRepository : GenericViewRepository<AccountBalancesTransaction>, IAccountBalancesTransactionRepository
    {
        private readonly DbSet<AccountBalancesTransaction> _accountBalancesTransactions;

        public AccountBalancesTransactionRepository(AppDbContext context) : base(context)
        {
            _accountBalancesTransactions = context.Set<AccountBalancesTransaction>();
        }
    }
}
