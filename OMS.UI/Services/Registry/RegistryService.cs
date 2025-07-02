using static Microsoft.Win32.Registry;

namespace OMS.UI.Services.Registry
{
    public class RegistryService : IRegistryService
    {
        public enum SubRegistryPath { AppConfig, UserConfig }

        private readonly string _registryPath = @"Software\OMS\";

        public void SetRegistryValue(string name, string value, SubRegistryPath subPath)
        {
            using var keyHandle = CurrentUser.CreateSubKey(_registryPath + subPath.ToString());
            keyHandle?.SetValue(name, value);
        }

        public string? GetRegistryValue(string name, SubRegistryPath subPath)
        {
            using var keyHandle = CurrentUser.OpenSubKey(_registryPath + subPath.ToString());
            return keyHandle?.GetValue(name) as string;
        }
    }
}
