using AutoMapper;
using OMS.UI.APIs.Dtos.StoredProcedureParams;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace OMS.UI.APIs.Services.Tables
{
    public class SaleService : GenericApiService<SaleDto, SaleModel>, ISaleService
    {
        public SaleService(IHttpClientFactory httpClientFactory, IMapper mapper)
                           : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Sales)
        {

        }

        public async Task<bool> AddSaleAsync(CreateSaleModel model)
        {
            try
            {
                var dto = _mapper.Map<CreateSaleDto>(model);
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}", dto);

                if (response.IsSuccessStatusCode)
                {
                    var createdSaleDto = await response.Content.ReadFromJsonAsync<CreateSaleDto>();
                    return (model.SaleId = createdSaleDto!.SaleId) > 0;
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

        public override Task<bool> AddAsync(SaleModel model)
            => throw new NotImplementedException("AddAsync is disabled for SaleService.");
    }
}
