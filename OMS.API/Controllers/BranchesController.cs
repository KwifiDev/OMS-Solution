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
        [HttpGet("option")]
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






        /// <summary>
        /// Gets the unique identifier from the BranchModel.
        /// </summary>
        /// <param name="model">The BranchModel instance.</param>
        /// <returns>The branch's identifier.</returns>
        protected override int GetModelId(BranchModel model) => model.BranchId;

        /// <summary>
        /// Sets the identifier in the BranchDto.
        /// </summary>
        /// <param name="dto">The BranchDto instance.</param>
        /// <param name="id">The identifier to set.</param>
        protected override void SetDtoId(BranchDto dto, int id) => dto.BranchId = id;

        /// <summary>
        /// Retrieves all people from the service.
        /// </summary>
        /// <returns>A collection of BranchModel instances.</returns>
        protected override async Task<IEnumerable<BranchModel>> GetListOfModelsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Retrieves a specific branch by their ID.
        /// </summary>
        /// <param name="id">The ID of the branch to retrieve.</param>
        /// <returns>The requested BranchModel or null if not found.</returns>
        protected override async Task<BranchModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);

        /// <summary>
        /// Adds a new branch to the database.
        /// </summary>
        /// <param name="model">The BranchModel to add.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> AddModelAsync(BranchModel model) => await _service.AddAsync(model);

        /// <summary>
        /// Updates an existing branch in the database.
        /// </summary>
        /// <param name="model">The BranchModel with updated data.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> UpdateModelAsync(BranchModel model) => await _service.UpdateAsync(model);

        /// <summary>
        /// Deletes a branch from the database.
        /// </summary>
        /// <param name="id">The ID of the branch to delete.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);

        /// <summary>
        /// Verifies that the ID matches the branch ID in the DTO.
        /// </summary>
        /// <param name="id">The ID to verify.</param>
        /// <param name="dto">The BranchDto containing the branch ID.</param>
        /// <returns>True if the IDs match, otherwise false.</returns>
        protected override bool IsIdentifierIdentical(int id, BranchDto dto) => id == dto.BranchId;

        /// <summary>
        /// Check a branch from the database.
        /// </summary>
        /// <param name="id">The ID of the branch to check if is exist.</param>
        /// <returns>True if the branch exist, otherwise false.</returns>
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
    }
}
