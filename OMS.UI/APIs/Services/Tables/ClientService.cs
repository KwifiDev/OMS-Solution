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
    public class ClientService : GenericApiService<ClientDto, ClientModel>, IClientService
    {

        public ClientService(IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Clients)
        {
        }

        public Task<bool> PayAllDebtsById(PayDebtsDto dto)
        {
            throw new NotImplementedException();
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
