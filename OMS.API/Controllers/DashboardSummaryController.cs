using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.Common.Data;

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
        /// Retrieves dashboard Data in one record.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET api/dashboardsummary
        /// Returns data in the system. Consider using filtering for large datasets.
        /// </remarks>
        /// <returns>dashboard data</returns>
        /// <response code="200">Returns the complete data of dashboard</response>
        /// <response code="500">If there was an internal server error</response>
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
