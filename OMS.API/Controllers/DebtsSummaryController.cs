using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.Common.Data;
using OMS.Common.Dtos.Views;
using OMS.Common.Extensions.Pagination;

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
        /// Retrieves a paged list of DebtsSummary for a specific client.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     GET /api/debtssummary/by-client/123?pageNumber=1&amp;pageSize=10
        /// Returns all available DebtsSummary records for the specified client.
        /// </remarks>
        /// <param name="clientId">The ID of the client to retrieve DebtsSummary for (must be a positive integer).</param>
        /// <param name="parameters">Pagination parameters (page number and page size).</param>
        /// <returns>Paged list of DebtsSummary for the specified client.</returns>
        /// <response code="200">Returns the paged list of DebtsSummary.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("by-client/{clientId:int}")]
        [Authorize(Policy = PermissionsData.DebtsSummary.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<DebtsSummaryDto>>> GetDebtsSummaryByClientIdAsync([FromRoute] int clientId, [FromQuery] PaginationParams parameters)
        {
            try
            {
                var pagedResult = await _service.GetByClientIdPagedAsync(clientId, parameters);
                return Ok(new PagedResult<DebtsSummaryDto>
                {
                    Items = _mapper.Map<List<DebtsSummaryDto>>(pagedResult.Items),
                    TotalItems = pagedResult.TotalItems,
                    PageNumber = pagedResult.PageNumber,
                    PageSize = pagedResult.PageSize
                });
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
        protected override async Task<PagedResult<DebtsSummaryModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<DebtsSummaryModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}
