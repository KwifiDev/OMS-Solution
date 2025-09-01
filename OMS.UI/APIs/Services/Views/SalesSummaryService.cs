using AutoMapper;
using OMS.Common.Extensions.Pagination;
using OMS.UI.APIs.Dtos.Views;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Views;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Views
{
    public class SalesSummaryService : GenericViewApiService<SalesSummaryDto, SalesSummaryModel>, ISalesSummaryService
    {
        public SalesSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper)
                                   : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.SalesSummary)
        {
        }

        public async Task<PagedResult<SalesSummaryModel>?> GetSalesByClientIdPagedAsync(int clientId, PaginationParams parameters)
        {
            try
            {
                var queryString = BuildQueryString(parameters);
                var response = await _httpClient.GetAsync($"{_endpoint}/by-client/{clientId}?{queryString}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return null;
                }


                var pagedResultDto = await response.Content.ReadFromJsonAsync<PagedResult<SalesSummaryDto>>();
                return pagedResultDto == null ? new PagedResult<SalesSummaryModel>() :
                   new PagedResult<SalesSummaryModel>
                   {
                       Items = _mapper.Map<List<SalesSummaryModel>>(pagedResultDto.Items),
                       TotalItems = pagedResultDto.TotalItems,
                       PageNumber = pagedResultDto.PageNumber,
                       PageSize = pagedResultDto.PageSize
                   };
            }
            catch (HttpRequestException httpEx)
            {
                LogError(httpEx);
                return null;
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
    }
}
