using Microsoft.Extensions.Options;
using OMS.API.Models;
using OMS.API.Models.Settings;

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

            var tenantId = GetTenantIdFromJwtClaim();
            return GetTenantById(tenantId);
        }

        public TenantModel? GetFromHeader()
        {
            var tenantId = GetTenantIdFromHeader();
            if (tenantId is null) return null;

            return GetTenantById(tenantId);
        }

        public void SetTenant(TenantModel tenant) => CurrentTenant = tenant;

        private bool IsLocalHost() => _httpContext?.Request.Host.Host == "localhost" || (_httpContext?.Request.Host.Host == "127.0.0.1");

        private string? GetTenantIdFromJwtClaim() => _httpContext?.User.FindFirst("tenant_id")?.Value;

        private string? GetTenantIdFromHeader()
        {
            if (_httpContext?.Request.Headers.TryGetValue("X-Tenant-ID", out var headerValues) == true)
            {
                return headerValues.FirstOrDefault();
            }
            return null;
        }
    }
}
