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
        public async Task<IEnumerable<BranchOptionModel>> GetAllBranchesOption()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/options");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}"));
                    return Enumerable.Empty<BranchOptionModel>();
                }

                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<BranchOptionDto>>();
                return dto != null ? _mapper.Map<IEnumerable<BranchOptionModel>>(dto) : Enumerable.Empty<BranchOptionModel>();
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return Enumerable.Empty<BranchOptionModel>();
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

    }
}
