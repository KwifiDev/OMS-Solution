namespace OMS.UI.Services.Registry
{
    public interface IRegistryService
    {
        void SetUserLoginConfig(string username, string password);
        void SetAppConfig(string companyName, string description);
        void GetUserLoginConfig(out string? username, out string? password);
        void GetAppConfig(out string? companyName, out string? description);
        void ResetUserLoginConfig();
    }
}
