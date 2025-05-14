using AutoMapper;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.Dtos.Views;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Tables
{
    public class BranchService : GenericApiService<BranchDto, BranchModel>, IBranchService
    {
        public BranchService(IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Branches)
        {
        }
        public async Task<IEnumerable<BranchOption>> GetAllBranchesOption()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/option");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}"));
                    return Enumerable.Empty<BranchOption>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<BranchOptionDto>>();
                return dto != null ? _mapper.Map<IEnumerable<BranchOption>>(dto) : Enumerable.Empty<BranchOption>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<BranchOption>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

    }
}
