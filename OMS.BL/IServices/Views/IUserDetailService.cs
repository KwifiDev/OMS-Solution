using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IUserDetailService
    {
        Task<IEnumerable<UserDetailModel>> GetAllUsersDetailAsync();
        Task<UserDetailModel?> GetUserDetailByIdAsync(int userId);
    }
}
