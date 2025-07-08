using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class AccountBalancesTransactionService : GenericViewService<AccountBalancesTransaction, AccountBalancesTransactionModel>, IAccountBalancesTransactionService
    {
        private readonly IAccountBalancesTransactionRepository _accountBalancesTransactionRepository;

        public AccountBalancesTransactionService(IGenericViewRepository<AccountBalancesTransaction> genericRepo,
                                                 IMapperService mapper,
                                                 IAccountBalancesTransactionRepository repository) : base(genericRepo, mapper)
        {
            _accountBalancesTransactionRepository = repository;
        }

    }
}
