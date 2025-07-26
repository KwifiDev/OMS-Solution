using AutoMapper;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Interfaces.Tables;
using System.Net.Http;

namespace OMS.UI.APIs.Services.Tables
{
    public class RoleService : IRoleService
    {

        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;


        public RoleService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _endpoint = ApiEndpoints.Roles;
        }
    }
}
