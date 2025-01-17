using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class UserDetailService : GenericViewService<UserDetail, UserDetailDto>, IUserDetailService
    {
        private readonly IUserDetailRepository _userDetailRepository;

        public UserDetailService(IGenericViewRepository<UserDetail> genericRepo,
                                 IMapperService mapper,
                                 IUserDetailRepository repository) : base(genericRepo, mapper)
        {
            _userDetailRepository = repository;
        }


        /*
                 public async Task<IEnumerable<UserDetailDto>> GetAllUsersDetailAsync()
        {
            IEnumerable<UserDetail> userDetail = await _repository.GetAllAsync();

            return userDetail?.Select(u => new UserDetailDto
            {
                UserId = u.UserId,
                EmployeeName = u.EmployeeName,
                Username = u.Username,
                IsActive = u.IsActive,
                WorkingBranch = u.WorkingBranch

            }) ?? Enumerable.Empty<UserDetailDto>();
        }

        public async Task<UserDetailDto?> GetUserDetailByIdAsync(int userId)
        {
            UserDetail? user = await _repository.GetByIdAsync(userId);

            return user == null ? null : new UserDetailDto
            {
                UserId = user.UserId,
                EmployeeName = user.EmployeeName,
                Username = user.Username,
                IsActive = user.IsActive,
                WorkingBranch = user.WorkingBranch
            };
        }
         */

    }
}
