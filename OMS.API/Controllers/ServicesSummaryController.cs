using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    [Route("api/servicessummary")]
    [ApiController]
    public class ServicesSummaryController : GenericViewController<IServicesSummaryService, ServicesSummaryDto, ServicesSummaryModel>
    {
        public ServicesSummaryController(IServicesSummaryService service, IMapper mapper) : base(service, mapper)
        {
        }



        #region override abstract Methods
        protected override async Task<IEnumerable<ServicesSummaryModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<ServicesSummaryModel?> GetModelByIdAsync(int serviceId) => await _service.GetByIdAsync(serviceId);
        #endregion
    }
}
