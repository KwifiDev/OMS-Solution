using AutoMapper;
using OMS.Common.Extensions.Pagination;
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

        public virtual async Task<PagedResult<TModel>?> GetPagedAsync(PaginationParams parameters)
        {
            try
            {
                var queryString = BuildQueryString(parameters);
                var response = await _httpClient.GetAsync($"{_endpoint}?{queryString}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return null;
                }

                var dtoPagedResult = await response.Content.ReadFromJsonAsync<PagedResult<TDto>>();
                return dtoPagedResult == null ? null :
                    new PagedResult<TModel>
                    {
                        Items = _mapper.Map<List<TModel>>(dtoPagedResult.Items),
                        TotalItems = dtoPagedResult.TotalItems,
                        PageNumber = dtoPagedResult.PageNumber,
                        PageSize = dtoPagedResult.PageSize

                    };
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return new PagedResult<TModel>();
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


        protected string BuildQueryString(PaginationParams parameters)
            => $"PageNumber={parameters.PageNumber}&PageSize={parameters.PageSize}";


        protected void LogError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}", "Response Server", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
