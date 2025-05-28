using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class TransactionsByTypeRepository : GenericViewRepository<TransactionsByType>, ITransactionsByTypeRepository
    {
        public TransactionsByTypeRepository(AppDbContext context) : base(context)
        {

        }
    }
}
