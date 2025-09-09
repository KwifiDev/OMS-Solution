using AutoMapper;
using OMS.Common.Dtos.StoredProcedureParams;
using OMS.Common.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;
using OMS.UI.Services.WinLogger;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Tables
{
    public class SaleService : GenericApiService<SaleDto, SaleModel>, ISaleService
    {
        public SaleService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
                           : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Sales, logService)
        {

        }

        public async Task<bool> AddSaleAsync(SaleCreationModel model)
        {
            try
            {
                var dto = _mapper.Map<SaleCreationDto>(model);
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}", dto);

                if (response.IsSuccessStatusCode)
                {
                    var createdSaleDto = await response.Content.ReadFromJsonAsync<SaleCreationDto>();
                    return (model.Id = createdSaleDto!.SaleId) > 0;
                }

                LogError(new Exception($"خطأ في عملية المناقلة على الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                return false;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }


        public async Task<bool> CancelSaleAsync(int saleId)
        {
            try
            {
                var response = await _httpClient.PatchAsync($"{_endpoint}/{saleId}/cancel", null);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في عملية المناقلة على الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
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

        public override Task<bool> AddAsync(SaleModel model)
            => throw new NotImplementedException("AddAsync is disabled for SaleService.");
    }
}
