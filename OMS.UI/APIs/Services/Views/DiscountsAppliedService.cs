using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Dtos.Views;
using AutoMapper;
using OMS.UI.APIs.EndPoints;
using System.Net.Http;
using System.Net.Http.Json;
using OMS.UI.Models.Views;

namespace OMS.UI.APIs.Services.Views
{
    public class DiscountsAppliedService : GenericViewApiService<DiscountsAppliedDto, DiscountsAppliedModel>, IDiscountsAppliedService
    {

        public DiscountsAppliedService(IHttpClientFactory httpClientFactory, IMapper mapper)
                                       : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.DiscountsApplied)
        {
        }

        public async Task<IEnumerable<DiscountsAppliedModel>> GetDiscountsByServiceIdAsync(int serviceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/by-service/{serviceId}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return Enumerable.Empty<DiscountsAppliedModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<DiscountsAppliedDto>>();
                return dto != null ? _mapper.Map<IEnumerable<DiscountsAppliedModel>>(dto) : Enumerable.Empty<DiscountsAppliedModel>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<DiscountsAppliedModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

    }
}
