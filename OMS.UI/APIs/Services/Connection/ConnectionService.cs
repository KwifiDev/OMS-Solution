using OMS.UI.APIs.EndPoints;
using OMS.UI.Services.HttpHeaderManager;
using OMS.UI.Services.ShowMassage;
using System.Net.Http;

namespace OMS.UI.APIs.Services.Connection
{
    public class ConnectionService : IConnectionService
    {
        private readonly HttpClient _httpClient;
        private readonly IMessageService _messageService;
        private readonly IHttpHeaderManagerService _headerManagerService;
        private readonly string _endpoint;

        public ConnectionService(IHttpClientFactory httpClientFactory, IMessageService messageService, IHttpHeaderManagerService headerManagerService)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _messageService = messageService;
            _headerManagerService = headerManagerService;
            _endpoint = ApiEndpoints.HealthCheck;
        }

        public async Task<bool> VerifyServerConnectionAsync()
        {
            try
            {
                _headerManagerService.SetTenantIdInHeader(_httpClient);
                var response = await _httpClient.GetAsync($"{_endpoint}/status");

                if (!response.IsSuccessStatusCode)
                {
                    _messageService.ShowErrorMessage(
                        "Server & DB Connection Error",
                        $"Failed to connect to the server & database.\nStatus Code: {response.StatusCode}\nContent: {await response.Content.ReadAsStringAsync()}");

                    return false;
                }
            }
            catch (HttpRequestException httpEx)
            {
                _messageService.ShowErrorMessage(
                                "Server Connection Error",
                                $"Network error while connecting to the server: {httpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _messageService.ShowErrorMessage(
                                "Server Connection Error",
                                $"An unexpected error occurred: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}
