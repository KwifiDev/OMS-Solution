using OMS.UI.Services.Settings;
using System.Net.Http;

namespace OMS.UI.Services.HttpHeaderManager
{
    public class HttpHeaderManagerService : IHttpHeaderManagerService
    {
        private readonly ISettingsService _settings;

        public HttpHeaderManagerService(ISettingsService settings)
        {
            _settings = settings;
        }

        public void SetTenantIdInHeader(HttpClient httpClient)
        {
            UpdateHeader(httpClient, "X-Tenant-ID", _settings.TenantId);
        }

        private static void UpdateHeader(HttpClient httpClient, string key, string value)
        {
            if (httpClient.DefaultRequestHeaders.Contains(key))
                httpClient.DefaultRequestHeaders.Remove(key);

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
        }
    }
}
