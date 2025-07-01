using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    [Route("api/dashboardsummary")]
    [ApiController]
    public class DashboardSummaryController : GenericViewController<IDashboardSummaryService, DashboardSummaryDto, DashboardSummaryModel>
    {
        public DashboardSummaryController(IDashboardSummaryService service, IMapper mapper) : base(service, mapper)
        {
        }

        #region override abstract Methods
        protected override async Task<IEnumerable<DashboardSummaryModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override Task<DashboardSummaryModel?> GetModelByIdAsync(int id)
            => throw new NotImplementedException("This Future not supprted on this controller");
        #endregion
    }
}
