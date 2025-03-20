using OMS.UI.Models;

namespace OMS.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<UserLoginModel> AuthenticateAsync(string username, string password);
    }
}
