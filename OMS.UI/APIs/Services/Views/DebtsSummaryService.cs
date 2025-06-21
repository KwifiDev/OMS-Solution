using AutoMapper;
using OMS.UI.APIs.Dtos.Views;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Views
{
    public class DebtsSummaryService : GenericViewApiService<DebtsSummaryDto, DebtsSummaryModel>, IDebtsSummaryService
    {

        public DebtsSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.DebtsSummary)
        {
        }

        public async Task<IEnumerable<DebtsSummaryModel>> GetDebtsByClientIdAsync(int clientId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/by-client/{clientId}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return Enumerable.Empty<DebtsSummaryModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<DebtsSummaryDto>>();
                return dto != null ? _mapper.Map<IEnumerable<DebtsSummaryModel>>(dto) : Enumerable.Empty<DebtsSummaryModel>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<DebtsSummaryModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

    }
}
