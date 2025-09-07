using AutoMapper;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.WinLogger;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Tables
{
    public class RoleService : GenericApiService<RoleDto, RoleModel>, IRoleService
    {
        public RoleService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Roles, logService)
        {
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/all");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم :.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return Enumerable.Empty<RoleModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<RoleDto>>();
                return dto != null ? _mapper.Map<IEnumerable<RoleModel>>(dto) : Enumerable.Empty<RoleModel>(); ;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Enumerable.Empty<RoleModel>();
            }
        }

        public async Task<RoleModel?> GetByNameAsync(string roleName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/{roleName}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم للاسم: {roleName}.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return null;
                }

                var dto = await response.Content.ReadFromJsonAsync<RoleDto>();
                return dto != null ? _mapper.Map<RoleModel>(dto) : null;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }




    }
}
