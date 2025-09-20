namespace OMS.UI.Models.Others
{
    public class TenantSettings
    {
        public bool IsRemote { get; set; }
        public Tenant LocalTenant { get; set; } = null!;
        public Tenant RemoteTenant { get; set; } = null!;
    }
}
