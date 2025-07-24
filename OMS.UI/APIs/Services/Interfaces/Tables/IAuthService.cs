using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterModel model);
        Task<UserLoginModel?> SignInAsync(string username, string password);
        Task<bool> ChangePasswordAsync(ChangePasswordModel model);
        Task<bool> CreateUserAsync(UserModel model);
    }
}
