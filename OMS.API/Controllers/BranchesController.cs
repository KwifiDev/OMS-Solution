using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.Common.Data;
using OMS.Common.Dtos.Tables;
using OMS.Common.Dtos.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing branches data.
    /// </summary>
    [Authorize]
    [Route("api/branches")]
    [ApiController]
    public class BranchesController : GenericController<IBranchService, BranchDto, BranchModel>
    {

        /// <summary>
        /// Initializes a new instance of the BranchesController class.
        /// </summary>
        /// <param name="branchService">The branch service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public BranchesController(IBranchService branchService, IMapper mapper)
            : base(branchService, mapper)
        {
        }

        /// <summary>
        /// Retrieves all branch options (ID and Name only).
        /// </summary>
        /// <remarks>
        /// <code>
        /// GET /api/branches/options
        /// </code>
        /// </remarks>
        /// <returns>List of all branch options.</returns>
        /// <response code="200">Returns the complete list of branch options.</response>
        /// <response code="500">Internal server error occurred.</response>
        [Authorize(Policy = PermissionsData.Branches.View)]
        [HttpGet("options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BranchOptionDto>>> GetAllBranchesOptionAsync()
        {
            try
            {
                var models = await _service.GetAllBranchesOption();
                return Ok(_mapper.Map<IEnumerable<BranchOptionDto>>(models));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving branches option",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }

        #region override abstract Methods
        protected override int GetModelId(BranchModel model) => model.Id;
        protected override void SetDtoId(BranchDto dto, int id) => dto.Id = id;
        protected override async Task<PagedResult<BranchModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<BranchModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(BranchModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(BranchModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, BranchDto dto) => id == dto.Id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}
