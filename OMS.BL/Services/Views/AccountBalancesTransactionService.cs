using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class AccountBalancesTransactionService : GenericViewService<AccountBalancesTransaction, AccountBalancesTransactionDto>, IAccountBalancesTransactionService
    {
        private readonly IAccountBalancesTransactionRepository _accountBalancesTransactionRepository;

        public AccountBalancesTransactionService(IGenericViewRepository<AccountBalancesTransaction> genericRepo,
                                                 IMapperService mapper,
                                                 IAccountBalancesTransactionRepository repository) : base(genericRepo, mapper)
        {
            _accountBalancesTransactionRepository = repository;
        }


        /*
                 public async Task<IEnumerable<AccountBalancesTransactionDto>> GetAllAccountsBalancesTransactionsAsync()
        {
            IEnumerable<AccountBalancesTransaction> accountBalancesTransaction = await _repository.GetAllAsync();

            return accountBalancesTransaction?.Select(a => new AccountBalancesTransactionDto
            {
                AccountId = a.AccountId,
                ClientName = a.ClientName,
                UserAccount = a.UserAccount,
                AccountBalance = a.AccountBalance,
                TotalTransactions = a.TotalTransactions,
                TotalTransactionAmount = a.TotalTransactionAmount

            }) ?? Enumerable.Empty<AccountBalancesTransactionDto>();
        }

        public async Task<AccountBalancesTransactionDto?> GetAccountBalancesTransactionByIdAsync(int accountId)
        {
            AccountBalancesTransaction? account = await _repository.GetByIdAsync(accountId);

            return account == null ? null : new AccountBalancesTransactionDto
            {
                AccountId = account.AccountId,
                ClientName = account.ClientName,
                UserAccount = account.UserAccount,
                AccountBalance = account.AccountBalance,
                TotalTransactions = account.TotalTransactions,
                TotalTransactionAmount = account.TotalTransactionAmount
            };
        }
         */

    }
}
