using static OMS.UI.Services.Registry.RegistryService;

namespace OMS.UI.Services.Registry
{
    public interface IRegistryService
    {
        void SetRegistryValue(string name, string value, SubRegistryPath subPath);
        string? GetRegistryValue(string name, SubRegistryPath subPath);
    }
}
