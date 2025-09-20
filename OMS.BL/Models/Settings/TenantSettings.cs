using OMS.BL.Models.Hybrid;

namespace OMS.BL.Models.Settings
{
    public class TenantSettings
    {
        public List<TenantModel> Tenants { get; set; } = [];
        public TenantModel LocalTenant { get; set; } = new();
    }
}
