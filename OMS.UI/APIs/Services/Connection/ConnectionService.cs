using OMS.UI.APIs.EndPoints;
using System.Net.Http;
using System.Windows;

namespace OMS.UI.APIs.Services.Connection
{
    public class ConnectionService : IConnectionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public ConnectionService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _endpoint = ApiEndpoints.HealthCheck;
        }

        public async Task<bool> VerifyServerConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/status");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Failed to connect to the server & database.\nStatus Code: {response.StatusCode}\nContent: {await response.Content.ReadAsStringAsync()}",
                                    "Server & DB Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Network error while connecting to the server: {httpEx.Message}",
                                "Server Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}",
                                "Server Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
