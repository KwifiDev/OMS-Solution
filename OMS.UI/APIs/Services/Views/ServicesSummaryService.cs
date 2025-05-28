using AutoMapper;
using OMS.UI.APIs.Dtos.Views;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using System.Net.Http;

namespace OMS.UI.APIs.Services.Views
{
    public class ServicesSummaryService : GenericViewApiService<ServicesSummaryDto, ServicesSummaryModel>, IServicesSummaryService
    {
        public ServicesSummaryService(IHttpClientFactory httpClientFactory, IMapper mapper)
                                      : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.ServicesSummary)
        {
        }
    }
}
