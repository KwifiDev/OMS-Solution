using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel?> GetUserByIdAsync(int userId);
        Task<bool> AddUserAsync(UserModel model);
        Task<bool> UpdateUserAsync(UserModel model);
        Task<bool> DeleteUserAsync(int userId);
    }
}
