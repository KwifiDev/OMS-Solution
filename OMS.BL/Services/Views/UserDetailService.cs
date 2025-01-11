using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IUserDetailRepository _repository;

        public UserDetailService(IUserDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserDetailModel>> GetAllUsersDetailAsync()
        {
            IEnumerable<UserDetail> userDetail = await _repository.GetAllAsync();

            return userDetail?.Select(u => new UserDetailModel
            {
                UserId = u.UserId,
                EmployeeName = u.EmployeeName,
                Username = u.Username,
                IsActive = u.IsActive,
                WorkingBranch = u.WorkingBranch

            }) ?? Enumerable.Empty<UserDetailModel>();
        }

        public async Task<UserDetailModel?> GetUserDetailByIdAsync(int userId)
        {
            UserDetail? user = await _repository.GetUserDetailByIdAsync(userId);

            return user == null ? null : new UserDetailModel
            {
                UserId = user.UserId,
                EmployeeName = user.EmployeeName,
                Username = user.Username,
                IsActive = user.IsActive,
                WorkingBranch = user.WorkingBranch
            };
        }

    }
}
