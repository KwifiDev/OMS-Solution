using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IPermissionsConfigService
    {
        Task<IEnumerable<PermissionsConfigModel>> GetAllPermissionsConfigsAsync();
        Task<PermissionsConfigModel?> GetPermissionsConfigByIdAsync(int permissionsConfigId);
    }
}
