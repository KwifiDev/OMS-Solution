using System.Net.Http;

namespace OMS.UI.Services.HttpHeaderManager
{
    public interface IHttpHeaderManagerService
    {
        void SetTenantIdInHeader(HttpClient httpClient);
    }
}
