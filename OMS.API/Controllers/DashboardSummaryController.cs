using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Views;
using OMS.Common.Data;
using OMS.Common.Dtos.Views;

namespace OMS.API.Controllers
{
    [Authorize]
    [Route("api/dashboardsummary")]
    [ApiController]
    public class DashboardSummaryController : ControllerBase
    {
        private readonly IDashboardSummaryService _dashboardSummaryService;
        private readonly IMapper _mapper;

        public DashboardSummaryController(IDashboardSummaryService dashboardSummaryService, IMapper mapper)
        {
            _dashboardSummaryService = dashboardSummaryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves the dashboard summary data as a single record.
        /// </summary>
        /// <remarks>
        /// <code>
        /// GET /api/dashboardsummary
        /// </code>
        /// </remarks>
        /// <returns>The dashboard summary data, or no content if not available.</returns>
        /// <response code="200">Returns the dashboard summary data.</response>
        /// <response code="204">No dashboard data available.</response>
        /// <response code="500">Internal server error occurred.</response>
        [HttpGet]
        [Authorize(Policy = PermissionsData.Dashboard.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DashboardSummaryDto?>> GetData()
        {
            try
            {
                var model = await _dashboardSummaryService.GetData();

                if (model is null) return NoContent();

                return Ok(_mapper.Map<DashboardSummaryDto>(model));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving entities",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }
    }
}
