using AutoMapper;
using OMS.Common.Enums;
using OMS.UI.APIs.Dtos.StoredProcedureParams;
using OMS.UI.APIs.Dtos.Tables;
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
    public class ClientService : GenericApiService<ClientDto, ClientModel>, IClientService
    {

        public ClientService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Clients, logService)
        {
        }

        public async Task<EnPayDebtStatus> PayAllDebtsById(PayDebtsModel model)
        {
            try
            {
                var dto = _mapper.Map<PayDebtsDto>(model);
                var response = await _httpClient.PostAsJsonAsync($"{_endpoint}/pay", dto);

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في عملية المناقلة على الخادم.\nStatus Code: {response.StatusCode}\nContent:\n{await response.Content.ReadAsStringAsync()}"));
                    return EnPayDebtStatus.Failed;
                }

                return (EnPayDebtStatus)(await response.Content.ReadFromJsonAsync<int>());
            }
            catch (Exception ex)
            {
                LogError(ex);
                return default;
            }
        }


        public async Task<ClientModel?> GetByPersonIdAsync(int personId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/by-person/{personId}");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في المعرف الشخصي.\nStatus Code: {response.StatusCode}"));
                    return null;
                }

                var clientDto = await response.Content.ReadFromJsonAsync<ClientDto>();

                return _mapper.Map<ClientModel>(clientDto);

            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }

        }

        public async Task<int> GetIdByPersonIdAsync(int personId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/{personId}/id");

                if (!response.IsSuccessStatusCode)
                {
                    LogError(new Exception($"خطأ في المعرف الشخصي.\nStatus Code: {response.StatusCode}"));
                    return -1;
                }

                return await response.Content.ReadFromJsonAsync<int>();

            }
            catch (Exception ex)
            {
                LogError(ex);
                return -1;
            }

        }
    }
}
