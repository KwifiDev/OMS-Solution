using OMS.BL.Models.Hybrid;

namespace OMS.BL.IServices.Tables
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterModel model);
        Task<UserLoginModel?> SignInAsync(RequestLoginModel model);
        Task<bool> ChangePasswordAsync(ChangePasswordModel model);
        Task<bool> RegisterWithPersonAsync(FullRegisterModel model);
    }
}
