using static Microsoft.Win32.Registry;

namespace OMS.UI.Services.Registry
{
    public class RegistryService : IRegistryService
    {
        public enum SubRegistryPath { AppConfig, UserConfig }

        private readonly string _registryPath = @"Software\OMS\";

        private readonly string _username = "Username";
        private readonly string _password = "Password";

        private readonly string _companyName = "CompanyName";
        private readonly string _description = "Description";

        private void SetRegistryValue(string name, string value, SubRegistryPath subPath)
        {
            using var keyHandle = CurrentUser.CreateSubKey(_registryPath + subPath.ToString());
            keyHandle?.SetValue(name, value);
        }

        private string? GetRegistryValue(string name, SubRegistryPath subPath)
        {
            using var keyHandle = CurrentUser.OpenSubKey(_registryPath + subPath.ToString());
            return keyHandle?.GetValue(name) as string;
        }

        public void SetUserLoginConfig(string username, string password)
        {
            SetRegistryValue(_username, username, SubRegistryPath.UserConfig);
            SetRegistryValue(_password, password, SubRegistryPath.UserConfig);
        }

        public void SetAppConfig(string companyName, string description)
        {
            SetRegistryValue(_companyName, companyName, SubRegistryPath.AppConfig);
            SetRegistryValue(_description, description, SubRegistryPath.AppConfig);
        }

        public void GetUserLoginConfig(out string? username, out string? password)
        {
            username = GetRegistryValue(_username, SubRegistryPath.UserConfig);
            password = GetRegistryValue(_password, SubRegistryPath.UserConfig);
        }

        public void GetAppConfig(out string? companyName, out string? description)
        {
            companyName = GetRegistryValue(_companyName, SubRegistryPath.AppConfig);
            description = GetRegistryValue(_description, SubRegistryPath.AppConfig);
        }

        public void ResetUserLoginConfig()
        {
            SetRegistryValue(_username, string.Empty, SubRegistryPath.UserConfig);
            SetRegistryValue(_password, string.Empty, SubRegistryPath.UserConfig);
        }
    }
}
