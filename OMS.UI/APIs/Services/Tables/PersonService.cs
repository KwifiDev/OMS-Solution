using AutoMapper;
using OMS.Common.Dtos.Tables;
using OMS.UI.APIs.EndPoints;
using OMS.UI.APIs.Services.Generices;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.WinLogger;
using System.Net.Http;

namespace OMS.UI.APIs.Services.Tables
{
    public class PersonService : GenericApiService<PersonDto, PersonModel>, IPersonService
    {

        public PersonService(IHttpClientFactory httpClientFactory, IMapper mapper, ILogService logService)
                           : base(httpClientFactory.CreateClient("ApiClient"), mapper, ApiEndpoints.People, logService)
        {
        }

    }
}
