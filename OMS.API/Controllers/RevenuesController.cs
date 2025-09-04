using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing revenues.
    /// </summary>
    [Authorize]
    [Route("api/revenues")]
    [ApiController]
    public class RevenuesController : GenericController<IRevenueService, RevenueDto, RevenueModel>
    {
        /// <summary>
        /// Initializes a new instance of the AccountsController class.
        /// </summary>
        /// <param name="accountService">The account service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public RevenuesController(IRevenueService accountService, IMapper mapper) : base(accountService, mapper)
        {
        }


        /// <summary>
        /// Checks if an revenues can added.
        /// </summary>
        /// <remarks>
        /// This operation is more efficient than GET for existence checks as it doesn't return the entity body.
        /// 
        /// Example:
        /// HEAD /api/revenues/
        /// </remarks>
        /// <returns>
        /// - 200 OK with empty body if can add a new revenue
        /// - 404 Not Found if revenue cant add to db
        /// - Appropriate error response for invalid requests
        /// </returns>
        /// <response code="200">accepte to add new revenue (returns empty response with headers)</response>
        /// <response code="404">If already there revene on this date</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpHead("canaddrevenue")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> HeadAsync()
        {
            try
            {
                var canAdd = await _service.CanAddRevenue();
                return canAdd ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        #region override abstract Methods
        protected override int GetModelId(RevenueModel model) => model.Id;
        protected override void SetDtoId(RevenueDto dto, int id) => dto.Id = id;
        protected override async Task<PagedResult<RevenueModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<RevenueModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(RevenueModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(RevenueModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, RevenueDto dto) => id == dto.Id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}
