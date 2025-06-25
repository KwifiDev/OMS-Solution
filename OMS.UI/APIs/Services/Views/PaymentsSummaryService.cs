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
    public class PaymentsSummaryService : GenericViewApiService<PaymentsSummaryDto, PaymentsSummaryModel>, IPaymentsSummaryService
    {
        public PaymentsSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper)
                                      : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.PaymentsSummary)
        {
        }

        public async Task<IEnumerable<PaymentsSummaryModel>> GetPaymentsByAccountIdAsync(int accountId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/accounts/{accountId}/payments");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}"));
                    return Enumerable.Empty<PaymentsSummaryModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<PaymentsSummaryDto>>();
                return dto != null ? _mapper.Map<IEnumerable<PaymentsSummaryModel>>(dto) : Enumerable.Empty<PaymentsSummaryModel>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<PaymentsSummaryModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
    }
}
