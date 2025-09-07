using AutoMapper;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.WinLogger;
using System.Net.Http;

namespace OMS.UI.APIs.Services.Tables
{
    public class RevenueService : GenericApiService<RevenueDto, RevenueModel>, IRevenueService
    {

        public RevenueService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
                            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Revenues, logService)
        {
        }

        public async Task<bool> CanAddOnThisDay()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Head, $"{_httpClient.BaseAddress}{_endpoint}/canaddrevenue");
                var response = await _httpClient.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }
    }
}
