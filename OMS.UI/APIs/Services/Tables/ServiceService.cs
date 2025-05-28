using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Dtos.Tables;
using AutoMapper;
using OMS.UI.APIs.EndPoints;
using System.Net.Http;
using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Tables
{
    public class ServiceService : GenericApiService<ServiceDto, ServiceModel>, IServiceService
    {
        public ServiceService(IHttpClientFactory httpClientFactory, IMapper mapper)
                              : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Services)
        {
        }

    }
}
