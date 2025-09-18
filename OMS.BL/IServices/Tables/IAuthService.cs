using OMS.BL.Models.Hybrid;
using OMS.Common.Enums;

namespace OMS.BL.IServices.Tables
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterModel model);
        Task<(TokenModel TokenInfo, UserLoginModel UserLogin, IEnumerable<string> claims)> LoginAsync(LoginModel model, string tenantId);
        Task<bool> ChangePasswordAsync(ChangePasswordModel model);
        Task<bool> RegisterUserWithProfileAsync(FullRegisterModel model);


        Task<TokenModel?> UpdateToken(int userId, string tenantId);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
        Task<IEnumerable<string>> GetUserClaimsAsync(int userId);
        Task<EnAuthResult> AddUserToRoleAsync(UserRoleModel model);
        Task<EnAuthResult> RemoveUserFromRoleAsync(UserRoleModel model);
        Task<bool> ChangeUserRolesAsync(InputUserRolesModel model);
        Task<bool?> IsUserInRoleAsync(UserRoleModel model);
    }
}
