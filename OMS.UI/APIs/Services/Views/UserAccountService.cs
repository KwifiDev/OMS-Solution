using AutoMapper;
using OMS.Common.Dtos.Views;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Views;
using OMS.UI.Services.WinLogger;
using System.Net.Http;

namespace OMS.UI.APIs.Services.Views
{
    public class UserAccountService : GenericViewApiService<UserAccountDto, UserAccountModel>, IUserAccountService
    {
        public UserAccountService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
                           : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.UsersAccount, logService)
        {
        }

    }
}
