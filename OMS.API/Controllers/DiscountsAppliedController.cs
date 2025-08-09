using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;

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
        /// Retrieves all DiscountsApplied by service Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/discountsapplied/by-service/123
        /// Returns all available DiscountsApplied by Service Id in the system.
        /// </remarks>
        /// <returns>List of all DiscountsApplied by Service id</returns>
        /// <response code="200">Returns the complete list of Discounts Applied</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("by-service/{serviceId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DiscountsAppliedDto>>> GetDiscountsAppliedByServiceIdAsync([FromRoute] int serviceId)
        {
            try
            {
                var models = await _service.GetByServiceIdAsync(serviceId);
                return Ok(_mapper.Map<IEnumerable<DiscountsAppliedDto>>(models));
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
        protected override async Task<IEnumerable<DiscountsAppliedModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<DiscountsAppliedModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        #endregion
    }
}
