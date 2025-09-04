using AutoMapper;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Tables
{
    public class PermissionService : GenericApiService<PermissionDto, PermissionModel>, IPermissionService
    {

        public PermissionService(IHttpClientFactory httpClientFactory, IMapper mapper)
                           : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Permissions)
        {
        }

        public async Task<IEnumerable<PermissionModel>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/all");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم :.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return Enumerable.Empty<PermissionModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<PermissionDto>>();
                return dto != null ? _mapper.Map<IEnumerable<PermissionModel>>(dto) : Enumerable.Empty<PermissionModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Enumerable.Empty<PermissionModel>();
            }
        }

    }
}
