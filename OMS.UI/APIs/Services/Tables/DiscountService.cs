using AutoMapper;
using OMS.UI.APIs.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using System.Net.Http;

namespace OMS.UI.APIs.Services.Tables
{
    public class DiscountService : GenericApiService<DiscountDto, DiscountModel>, IDiscountService
    {
        public DiscountService(IHttpClientFactory httpClientFactory, IMapper mapper)
                               : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.Discounts)
        {
        }

    }
}
