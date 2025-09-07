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
    public class TransactionsSummaryService : GenericViewApiService<TransactionsSummaryDto, TransactionsSummaryModel>, ITransactionsSummaryService
    {
        public TransactionsSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.TransactionsSummary, logService)
        {
        }

        public async Task<PagedResult<TransactionsSummaryModel>?> GetTransactionsByAccountIdPagedAsync(int accountId, PaginationParams parameters)
        {
            try
            {
                var queryString = BuildQueryString(parameters);
                var response = await _httpClient.GetAsync($"{_endpoint}/accounts/{accountId}/transactions?{queryString}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}"));
                    return null;
                }

                var pagedResultDto = await response.Content.ReadFromJsonAsync<PagedResult<TransactionsSummaryDto>>();
                return pagedResultDto == null ? new PagedResult<TransactionsSummaryModel>() :
                   new PagedResult<TransactionsSummaryModel>
                   {
                       Items = _mapper.Map<List<TransactionsSummaryModel>>(pagedResultDto.Items),
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
