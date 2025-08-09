using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

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
        protected override Task<IEnumerable<RolesSummaryModel>> GetListOfModelsAsync() => _service.GetAllAsync();
        protected override Task<RolesSummaryModel?> GetModelByIdAsync(int id) => _service.GetByIdAsync(id);
        #endregion
    }
}
