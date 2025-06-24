using AutoMapper;
using OMS.Common.Enums;
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
    public class DebtService : GenericApiService<DebtDto, DebtModel>, IDebtService
    {

        public DebtService(IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Debts)
        {
        }

        public async Task<bool> AddDebtAsync(DebtCreationModel model)
        {
            try
            {
                var dto = _mapper.Map<DebtCreationDto>(model);
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}", dto);

                if (response.IsSuccessStatusCode)
                {
                    var createdDebtDto = await response.Content.ReadFromJsonAsync<DebtCreationDto>();
                    return (model.DebtId = createdDebtDto!.DebtId) > 0;
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


        public async Task<bool> CancelDebtAsync(int debtId)
        {
            try
            {
                var response = await _httpClient.PatchAsync($"{_endpoint}/{debtId}/cancel", null);

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

        public override Task<bool> AddAsync(DebtModel model)
            => throw new NotImplementedException("AddAsync is disabled for DebtService.");


        public async Task<EnPayDebtStatus> PayDebtAsync(PayDebtModel model)
        {
            try
            {
                var dto = _mapper.Map<PayDebtDto>(model);
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/pay", dto);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في عملية المناقلة على الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                }

                return (EnPayDebtStatus)(await response.Content.ReadFromJsonAsync<int>());
            }
            catch (Exception ex)
            {
                LogError(ex);
                return default;
            }
        }


    }
}
