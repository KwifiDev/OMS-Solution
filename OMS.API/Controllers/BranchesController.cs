using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.API.Dtos.Views;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing branches data.
    /// </summary>
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
        /// Retrieves all branches option.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/branches/option
        ///     
        /// Returns all available branches option in the system.
        /// </remarks>
        /// <returns>List of all branches option</returns>
        /// <response code="200">Returns the complete list of branches option</response>
        /// <response code="500">If there was an internal server error</response>
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
        protected override int GetModelId(BranchModel model) => model.BranchId;
        protected override void SetDtoId(BranchDto dto, int id) => dto.BranchId = id;
        protected override async Task<IEnumerable<BranchModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<BranchModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(BranchModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(BranchModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, BranchDto dto) => id == dto.BranchId;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}
