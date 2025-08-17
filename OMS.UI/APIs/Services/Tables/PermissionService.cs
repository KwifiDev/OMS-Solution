using AutoMapper;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using System.Net.Http;

namespace OMS.UI.APIs.Services.Tables
{
    public class PermissionService : GenericApiService<PermissionDto, PermissionModel>, IPermissionService
    {

        public PermissionService(IHttpClientFactory httpClientFactory, IMapper mapper)
                           : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Permissions)
        {
        }

    }
}
