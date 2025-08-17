using Microsoft.Extensions.DependencyInjection;
using OMS.UI.Services.UserSession;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;

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
            int retryCount = 0;
            const int maxRetryCount = 5;
            const int delayMilliseconds = 1000;

            while (true)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var userSessionService = scope.ServiceProvider.GetRequiredService<IUserSessionService>();

                        if (userSessionService.IsLoggedIn &&
                            userSessionService.CurrentToken != null)
                        {
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userSessionService.CurrentToken.Token);
                        }
                    }
                    return await base.SendAsync(request, cancellationToken);
                }
                catch (HttpRequestException ex) when (ex.InnerException is SocketException && retryCount < maxRetryCount)
                {
                    retryCount++;
                    await Task.Delay(delayMilliseconds * retryCount, cancellationToken);
                }
            }
        }
    }
}