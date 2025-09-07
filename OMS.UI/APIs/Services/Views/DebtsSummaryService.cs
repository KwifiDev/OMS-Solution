using AutoMapper;
using OMS.Common.Extensions.Pagination;
using OMS.UI.APIs.Dtos.Views;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Views;
using OMS.UI.Services.WinLogger;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Views
{
    public class DebtsSummaryService : GenericViewApiService<DebtsSummaryDto, DebtsSummaryModel>, IDebtsSummaryService
    {

        public DebtsSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.DebtsSummary, logService)
        {
        }

        public async Task<PagedResult<DebtsSummaryModel>?> GetDebtsByClientIdPagedAsync(int clientId, PaginationParams parameters)
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

                var pagedResultDto = await response.Content.ReadFromJsonAsync<PagedResult<DebtsSummaryDto>>();
                return pagedResultDto == null ? new PagedResult<DebtsSummaryModel>() :
                    new PagedResult<DebtsSummaryModel>
                    {
                        Items = _mapper.Map<List<DebtsSummaryModel>>(pagedResultDto.Items),
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
