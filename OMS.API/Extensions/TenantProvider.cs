using Microsoft.Extensions.Options;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Settings;

namespace OMS.API.Extensions
{
    public class TenantProvider : ITenantProvider
    {
        private readonly HttpContext? _httpContext;
        private readonly TenantSettings _tenantSettings;

        public TenantProvider(IHttpContextAccessor httpContextAccessor, IOptions<TenantSettings> options)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _tenantSettings = options.Value;
        }

        private bool IsHttpContextValid => _httpContext?.User?.Identity?.IsAuthenticated == true;

        public TenantModel CurrentTenant { get; private set; } = null!;


        private TenantModel? GetTenantById(string? tenantId)
        {
            if (string.IsNullOrWhiteSpace(tenantId)) return null;
            return _tenantSettings.Tenants.FirstOrDefault(t => t.Id == tenantId);
        }

        public TenantModel? GetFromJwtClaim()
        {
            if (!IsHttpContextValid) return null;

            var tenantId = _httpContext?.User.FindFirst("tenant_id")?.Value;
            return GetTenantById(tenantId);
        }

        public TenantModel? GetFromHeader()
        {
            if (_httpContext?.Request.Headers.TryGetValue("X-Tenant-ID", out var headerValues) != true) return null;

            var tenantId = headerValues.FirstOrDefault();
            return GetTenantById(tenantId);
        }

        public void SetTenant(TenantModel tenant) => CurrentTenant = tenant;
        
    }
}
