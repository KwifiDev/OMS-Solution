using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class UserDetailService : GenericViewService<UserDetail, UserDetailModel>, IUserDetailService
    {
        private readonly IUserDetailRepository _userDetailRepository;

        public UserDetailService(IGenericViewRepository<UserDetail> genericRepo,
                                 IMapperService mapper,
                                 IUserDetailRepository repository) : base(genericRepo, mapper)
        {
            _userDetailRepository = repository;
        }

    }
}
