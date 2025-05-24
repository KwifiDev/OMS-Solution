using AutoMapper;
using OMS.UI.APIs.Services.Interfaces.Views;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace OMS.UI.APIs.Services.Generices
{
    public class GenericViewApiService<TDto, TModel> : IGenericViewApiService<TDto, TModel>
        where TDto : class
        where TModel : class
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _endpoint;
        protected readonly IMapper _mapper;


        public GenericViewApiService(HttpClient httpClient, IMapper mapper, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return Enumerable.Empty<TModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>();
                return dto != null ? _mapper.Map<IEnumerable<TModel>>(dto) : Enumerable.Empty<TModel>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<TModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public virtual async Task<TModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم للمعرف: {id}.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return null;
                }

                var dto = await response.Content.ReadFromJsonAsync<TDto>();
                return dto != null ? _mapper.Map<TModel>(dto) : null;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }


        protected void LogError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}","Response Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
