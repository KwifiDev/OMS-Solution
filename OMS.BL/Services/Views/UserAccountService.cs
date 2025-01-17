using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class UserAccountService : GenericViewService<UserAccount, UserAccountDto>, IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IGenericViewRepository<UserAccount> genericRepo,
                                  IMapperService mapper,
                                  IUserAccountRepository repository) : base(genericRepo, mapper)
        {
            _userAccountRepository = repository;
        }



        /*
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
         */

    }
}
