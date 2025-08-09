//using OMS.UI.Models.Others;
//using System.Net.Http;
//using System.Net.Http.Headers;

//namespace OMS.UI.APIs.Services.Security
//{
//    public class TokenService : ITokenService
//    {
//        private readonly HttpClient _httpClient;

//        public TokenService(IHttpClientFactory httpClientFactory)
//        {
//            _httpClient = httpClientFactory.CreateClient("ApiClient");
//        }

//        public bool SetAuthHeader(TokenModel tokenInfo)
//        {
//            if (tokenInfo is null || string.IsNullOrWhiteSpace(tokenInfo.Token)) return false;

//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenInfo.TokenType ?? "Bearer", tokenInfo.Token);
//            return true;
//        }

//        public void ResetAuthHeader()
//        {
//            _httpClient.DefaultRequestHeaders.Authorization = null;
//        }
//    }
//}
