namespace OMS.API.Models.Settings
{
    public class TenantSettings
    {
        public List<TenantModel> Tenants { get; set; } = [];
        public TenantModel LocalTenant { get; set; } = new();
    }
}
