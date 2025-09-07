using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Dtos.Tables;
using AutoMapper;
using OMS.UI.APIs.EndPoints;
using System.Net.Http;
using OMS.UI.APIs.Dtos.Views;
using System.Net.Http.Json;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Services.WinLogger;

namespace OMS.UI.APIs.Services.Tables
{
    public class ServiceService : GenericApiService<ServiceDto, ServiceModel>, IServiceService
    {
        public ServiceService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
                              : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Services, logService)
        {
        }

        public async Task<IEnumerable<ServiceOptionModel>> GetAllServicesOption()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/options");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}"));
                    return Enumerable.Empty<ServiceOptionModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<ServiceOptionDto>>();
                return dto != null ? _mapper.Map<IEnumerable<ServiceOptionModel>>(dto) : Enumerable.Empty<ServiceOptionModel>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<ServiceOptionModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

    }
}
