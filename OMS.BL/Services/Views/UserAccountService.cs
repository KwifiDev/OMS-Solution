using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class UserAccountService : GenericViewService<UserAccount, UserAccountModel>, IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IGenericViewRepository<UserAccount> genericRepo,
                                  IMapperService mapper,
                                  IUserAccountRepository repository) : base(genericRepo, mapper)
        {
            _userAccountRepository = repository;
        }

    }
}
