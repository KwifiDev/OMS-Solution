using Microsoft.Extensions.DependencyInjection;
using OMS.UI.Services.UserSession;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OMS.UI.APIs.Services.Security
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public AuthHeaderHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userSessionService = scope.ServiceProvider.GetRequiredService<IUserSessionService>();

                if (userSessionService.IsLoggedIn &&
                    userSessionService.CurrentUser?.TokenInfo != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userSessionService.CurrentUser.TokenInfo.Token);
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}