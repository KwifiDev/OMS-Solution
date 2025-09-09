using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.Common.Dtos.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    [Authorize]
    [Route("api/rolessummary")]
    [ApiController]
    public class RolesSummaryController : GenericViewController<IRolesSummaryService, RolesSummaryDto, RolesSummaryModel>
    {
        public RolesSummaryController(IRolesSummaryService service, IMapper mapper) : base(service, mapper)
        {
        }

        #region Common Abstract Methods
        protected override async Task<PagedResult<RolesSummaryModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override Task<RolesSummaryModel?> GetModelByIdAsync(int id) => _service.GetByIdAsync(id);
        #endregion
    }
}
