using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterModel model);
        Task<UserLoginModel?> SignInAsync(string username, string password);
        Task<bool> ChangePasswordAsync(ChangePasswordModel model);
        Task<bool> CreateUserAsync(UserModel model);
        Task<IEnumerable<string>> GetUserRolesByUserIdAsync(int userId);
        Task<bool> AddUserToRoleAsync(int userId, string roleName);
        Task<bool> RemoveUserFromRoleAsync(int userId, string roleName);
        Task<bool> IsUserInRoleAsync(int userId, string roleName);
    }
}
