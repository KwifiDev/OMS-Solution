using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
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

        public async Task<IEnumerable<UserAccountModel>> GetAllUsersAccountsAsync()
        {
            IEnumerable<UserAccount> userAccount = await _repository.GetAllAsync();

            return userAccount?.Select(a => new UserAccountModel
            {
               AccountId = a.AccountId,
               UserAccount1 = a.UserAccount1,
               ClientName = a.ClientName,
               ClientType = a.ClientType,
               ClientBalance = a.ClientBalance

            }) ?? Enumerable.Empty<UserAccountModel>();
        }

        public async Task<UserAccountModel?> GetUserAccountByIdAsync(int accountId)
        {
            UserAccount? userAccount = await _repository.GetUserAccountByIdAsync(accountId);

            return userAccount == null ? null : new UserAccountModel
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
