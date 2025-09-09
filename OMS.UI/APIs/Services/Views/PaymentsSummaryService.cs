using AutoMapper;
using OMS.Common.Dtos.Views;
using OMS.Common.Extensions.Pagination;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Views;
using OMS.UI.Services.WinLogger;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Views
{
    public class PaymentsSummaryService : GenericViewApiService<PaymentsSummaryDto, PaymentsSummaryModel>, IPaymentsSummaryService
    {
        public PaymentsSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
                                      : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.PaymentsSummary, logService)
        {
        }

        public async Task<PagedResult<PaymentsSummaryModel>?> GetPaymentsByAccountIdPagedAsync(int accountId, PaginationParams parameters)
        {
            try
            {
                var queryString = BuildQueryString(parameters);
                var response = await _httpClient.GetAsync($"{_endpoint}/accounts/{accountId}/payments?{queryString}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في جلب البيانات من الخادم.\nStatus Code: {response.StatusCode}"));
                    return null;
                }

                var pagedResultDto = await response.Content.ReadFromJsonAsync<PagedResult<PaymentsSummaryDto>>();
                return pagedResultDto == null ? new PagedResult<PaymentsSummaryModel>() :
                   new PagedResult<PaymentsSummaryModel>
                   {
                       Items = _mapper.Map<List<PaymentsSummaryModel>>(pagedResultDto.Items),
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
