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
    [Route("api/discountsapplied")]
    [ApiController]
    public class DiscountsAppliedController : GenericViewController<IDiscountsAppliedService, DiscountsAppliedDto, DiscountsAppliedModel>
    {
        public DiscountsAppliedController(IDiscountsAppliedService service, IMapper mapper) : base(service, mapper)
        {
        }



        /// <summary>
        /// Retrieves a paged list of DiscountsApplied for a specific service.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     GET /api/discountsapplied/by-service/123?pageNumber=1&amp;pageSize=10
        /// Returns all DiscountsApplied records for the specified service.
        /// </remarks>
        /// <param name="serviceId">The ID of the service to retrieve DiscountsApplied for (must be a positive integer).</param>
        /// <param name="parameters">Pagination parameters (page number and page size).</param>
        /// <returns>Paged list of DiscountsApplied for the specified service.</returns>
        /// <response code="200">Returns the paged list of DiscountsApplied.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("by-service/{serviceId:int}")]
        [Authorize(Policy = PermissionsData.DiscountsApplied.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<DiscountsAppliedDto>>> GetDiscountsAppliedByServiceIdAsync([FromRoute] int serviceId, [FromQuery] PaginationParams parameters)
        {
            try
            {
                var pagedResult = await _service.GetByServiceIdPagedAsync(serviceId, parameters);
                return Ok(new PagedResult<DiscountsAppliedDto>
                {
                    Items = _mapper.Map<List<DiscountsAppliedDto>>(pagedResult.Items),
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
        protected override async Task<PagedResult<DiscountsAppliedModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<DiscountsAppliedModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}
