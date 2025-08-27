using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    [Authorize]
    [Route("api/servicessummary")]
    [ApiController]
    public class ServicesSummaryController : GenericViewController<IServicesSummaryService, ServicesSummaryDto, ServicesSummaryModel>
    {
        public ServicesSummaryController(IServicesSummaryService service, IMapper mapper) : base(service, mapper)
        {
        }



        #region override abstract Methods
        protected override async Task<PagedResult<ServicesSummaryModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<ServicesSummaryModel?> GetModelByIdAsync(int serviceId) => await _service.GetByIdAsync(serviceId);
        #endregion
    }
}
