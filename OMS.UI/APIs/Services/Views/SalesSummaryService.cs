using AutoMapper;
using OMS.UI.APIs.Dtos.Views;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Views;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Views
{
    public class SalesSummaryService : GenericViewApiService<SalesSummaryDto, SalesSummaryModel>, ISalesSummaryService
    {
        public SalesSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper)
                                   : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.SalesSummary)
        {
        }

        public async Task<IEnumerable<SalesSummaryModel>> GetSalesByClientIdAsync(int clientId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/by-client/{clientId}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return Enumerable.Empty<SalesSummaryModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<SalesSummaryDto>>();
                return dto != null ? _mapper.Map<IEnumerable<SalesSummaryModel>>(dto) : Enumerable.Empty<SalesSummaryModel>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<SalesSummaryModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
    }
}
