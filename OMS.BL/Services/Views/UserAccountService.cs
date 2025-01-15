using OMS.BL.IServices.Views;
using OMS.BL.Dtos.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _repository;

        public UserAccountService(IUserAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserAccountDto>> GetAllUsersAccountsAsync()
        {
            IEnumerable<UserAccount> userAccount = await _repository.GetAllAsync();

            return userAccount?.Select(a => new UserAccountDto
            {
               AccountId = a.AccountId,
               UserAccount1 = a.UserAccount1,
               ClientName = a.ClientName,
               ClientType = a.ClientType,
               ClientBalance = a.ClientBalance

            }) ?? Enumerable.Empty<UserAccountDto>();
        }

        public async Task<UserAccountDto?> GetUserAccountByIdAsync(int accountId)
        {
            UserAccount? userAccount = await _repository.GetByIdAsync(accountId);

            return userAccount == null ? null : new UserAccountDto
            {
                AccountId = userAccount.AccountId,
                UserAccount1 = userAccount.UserAccount1,
                ClientName = userAccount.ClientName,
                ClientType = userAccount.ClientType,
                ClientBalance = userAccount.ClientBalance
            };
        }

    }
}
