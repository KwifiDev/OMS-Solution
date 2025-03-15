using OMS.UI.Models;

namespace OMS.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<UserModel> AuthenticateAsync(string username, string password);
    }
}
