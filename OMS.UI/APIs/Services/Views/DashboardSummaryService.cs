using AutoMapper;
using OMS.UI.APIs.Dtos.Views;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Views;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace OMS.UI.APIs.Services.Views
{
    public class DashboardSummaryService : IDashboardSummaryService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly string _endpoint;

        public DashboardSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _mapper = mapper;
            _endpoint = ApiEndpoints.DashboardSummary;
        }

        public async Task<DashboardSummaryModel?> GetData()
        {
            try
            {
                var response = await _httpClient.GetAsync(_endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return default;
                }

                var dto = await response.Content.ReadFromJsonAsync<DashboardSummaryDto>();
                return dto != null ? _mapper.Map<DashboardSummaryModel>(dto) : default;
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return default;
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        private void LogError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}", "Response Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
