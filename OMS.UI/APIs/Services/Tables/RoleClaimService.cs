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
    public class RoleClaimService : GenericViewApiService<RoleClaimDto, RoleClaimModel>, IRoleClaimService
    {
        public RoleClaimService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.RoleClaims, logService)
        {
        }


        public async Task<IEnumerable<RoleClaimModel>> GetRoleClaimsByRoleIdAsync(int roleId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/by-role/{roleId}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return Enumerable.Empty<RoleClaimModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<RoleClaimDto>>();
                return dto != null ? _mapper.Map<IEnumerable<RoleClaimModel>>(dto) : Enumerable.Empty<RoleClaimModel>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<RoleClaimModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public async Task<bool> AddRoleClaimAsync(int roleId, string claimType, string claimValue)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/add/{roleId}", new { ClaimType = claimType, ClaimValue = claimValue });

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

        public async Task<bool> RemoveRoleClaimAsync(int roleId, string claimType, string claimValue)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/remove/{roleId}", new { ClaimType = claimType, ClaimValue = claimValue });

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
