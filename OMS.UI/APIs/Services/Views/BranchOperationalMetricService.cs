using AutoMapper;
using OMS.UI.APIs.Dtos.Views;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using System.Net.Http;

namespace OMS.UI.APIs.Services.Views
{
    public class BranchOperationalMetricService : GenericViewApiService<BranchOperationalMetricDto, BranchOperationalMetricModel>, IBranchOperationalMetricService
    {
        public BranchOperationalMetricService(IHttpClientFactory httpClientFactory, IMapper mapper)
                           : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.BranchesOperationalMetric)
        {
        }

    }
}
