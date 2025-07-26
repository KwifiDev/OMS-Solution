using OMS.BL.Models.Hybrid;
using OMS.Common.Enums;

namespace OMS.BL.IServices.Tables
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterModel model);
        Task<UserLoginModel?> LoginAsync(LoginModel model);
        Task<bool> ChangePasswordAsync(ChangePasswordModel model);
        Task<bool> RegisterUserWithProfileAsync(FullRegisterModel model);


        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
        Task<EnAuthResult> AddUserToRoleAsync(int userId, string roleName);
        Task<EnAuthResult> RemoveUserFromRoleAsync(int userId, string roleName);
        Task<bool?> IsUserInRoleAsync(int userId, string roleName);
    }
}
