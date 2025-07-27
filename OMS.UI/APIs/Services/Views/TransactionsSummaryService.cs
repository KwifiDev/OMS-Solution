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
    public class TransactionsSummaryService : GenericViewApiService<TransactionsSummaryDto, TransactionsSummaryModel>, ITransactionsSummaryService
    {
        public TransactionsSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.TransactionsSummary)
        {
        }

        public async Task<IEnumerable<TransactionsSummaryModel>> GetTransactionsByAccountIdAsync(int accountId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/accounts/{accountId}/transactions");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}"));
                    return Enumerable.Empty<TransactionsSummaryModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<TransactionsSummaryDto>>();
                return dto != null ? _mapper.Map<IEnumerable<TransactionsSummaryModel>>(dto) : Enumerable.Empty<TransactionsSummaryModel>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<TransactionsSummaryModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
    }
}
