using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    [Authorize]
    [Route("api/discounts")]
    [ApiController]
    public class DiscountsController : GenericController<IDiscountService, DiscountDto, DiscountModel>
    {
        public DiscountsController(IDiscountService service, IMapper mapper) : base(service, mapper)
        {
        }

        /// <summary>
        /// Creates a new Discount Dto.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/discounts
        ///     {
        ///         "name": "New discounts",
        ///         "description": "discounts description"
        ///     }
        /// </remarks>
        /// <param name="dto">The DTO containing data for the new discounts</param>
        /// <returns>The created discounts with generated ID</returns>
        /// <response code="201">Returns the newly created discounts</response>
        /// <response code="409">If the discount is already apply</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult<DiscountDto>> AddAsync([FromBody] DiscountDto dto)
        {
            try
            {
                var isAlreadyApplied = await _service.IsDiscountAlreadyApplied(_mapper.Map<CheckDiscountAppliedModel>(dto));

                if (isAlreadyApplied) return Conflict();

                var model = _mapper.Map<DiscountModel>(dto);
                var isSuccess = await AddModelAsync(model);

                if (!isSuccess)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to save discount in the database",
                        Errors = { { "General", new[] { "Failed to save discount in the database" } } }
                    });
                }

                var id = GetModelId(model);
                SetDtoId(dto, id);

                return CreatedAtAction(
                    actionName: "GetById",
                    routeValues: new { id },
                    value: dto);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error creating discount",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }

        #region override abstract Methods
        protected override async Task<bool> AddModelAsync(DiscountModel model) => await _service.AddAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override async Task<IEnumerable<DiscountModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<DiscountModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override int GetModelId(DiscountModel model) => model.DiscountId;
        protected override bool IsIdentifierIdentical(int id, DiscountDto dto) => dto.DiscountId == id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        protected override void SetDtoId(DiscountDto dto, int id) => dto.DiscountId = id;
        protected override async Task<bool> UpdateModelAsync(DiscountModel model) => await _service.UpdateAsync(model);
        #endregion
    }
}
