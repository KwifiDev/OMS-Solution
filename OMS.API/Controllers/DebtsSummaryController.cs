using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

namespace OMS.API.Controllers
{
    [Authorize]
    [Route("api/debtssummary")]
    [ApiController]
    public class DebtsSummaryController : GenericViewController<IDebtsSummaryService, DebtsSummaryDto, DebtsSummaryModel>
    {
        public DebtsSummaryController(IDebtsSummaryService service, IMapper mapper) : base(service, mapper)
        {
        }


        /// <summary>
        /// Retrieves all DebtsSummary by client Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/debtsSummary/by-client/123
        /// Returns all available DebtsSummary by Client Id in the system.
        /// </remarks>
        /// <returns>List of all DebtsSummary by Client id</returns>
        /// <response code="200">Returns the complete list of DebtsSummary</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("by-client/{clientId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DebtsSummaryDto>>> GetDebtsSummaryByClientIdAsync([FromRoute] int clientId)
        {
            try
            {
                var models = await _service.GetByClientIdAsync(clientId);
                return Ok(_mapper.Map<IEnumerable<DebtsSummaryDto>>(models));
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



        #region override abstract Methods
        protected override async Task<IEnumerable<DebtsSummaryModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<DebtsSummaryModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}
