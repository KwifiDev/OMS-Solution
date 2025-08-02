using AutoMapper;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace OMS.UI.APIs.Services.Tables
{
    public class RoleService : GenericApiService<RoleDto, RoleModel>, IRoleService
    {
        public RoleService(IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Roles)
        {
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


        public async Task<bool> AddRoleClaimAsync(int roleId, Claim claim)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/addroleclaim", claim);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في اضافة البيانات الى الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }

        public async Task<bool> RemoveRoleClaimAsync(int roleId, Claim claim)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/removeroleclaim", claim);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في اضافة البيانات الى الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }
    }
}
