using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace OMS.API.Controllers
{
    /// <summary>
    /// Abstract base controller that provides common CRUD operations for API controllers.
    /// </summary>
    /// <typeparam name="TService">The service interface type used for business logic operations.</typeparam>
    /// <typeparam name="TDto">The Data Transfer Object (DTO) type used for API requests/responses.</typeparam>
    /// <typeparam name="TModel">The domain model type used for database operations.</typeparam>
    public abstract class BaseController<TService, TDto, TModel> : ControllerBase
        where TService : class
        where TDto : class
        where TModel : class
    {
        /// <summary>
        /// The service instance for handling business logic.
        /// </summary>
        protected readonly TService _service;

        /// <summary>
        /// The AutoMapper instance for object-object mapping.
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the BaseController class.
        /// </summary>
        /// <param name="service">The service instance for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public BaseController(TService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>A list of all entities mapped to DTOs.</returns>
        /// <response code="200">Returns the list of entities.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TDto>>> GetAllAsync()
        {
            try
            {
                var models = await GetListOfModelsAsync();
                return Ok(_mapper.Map<IEnumerable<TDto>>(models));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieves a specific entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The requested entity mapped to DTO.</returns>
        /// <response code="200">Returns the requested entity.</response>
        /// <response code="400">If the ID is invalid (less than or equal to 0).</response>
        /// <response code="404">If no entity was found with the specified ID.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpGet("{id:int}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TDto>> GetByIdAsync([FromRoute] int id)
        {
            if (id <= 0) return BadRequest($"Invalid Id: [{id}]");

            try
            {
                var model = await GetModelByIdAsync(id);
                return model != null
                    ? Ok(_mapper.Map<TDto>(model))
                    : NotFound($"No entity found with ID: [{id}]");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="dto">The DTO containing data for the new entity.</param>
        /// <returns>The created entity mapped to DTO.</returns>
        /// <response code="201">Returns the newly created entity.</response>
        /// <response code="400">If the entity creation failed.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TDto>> AddAsync([FromBody] TDto dto)
        {
            try
            {
                var model = _mapper.Map<TModel>(dto);
                var isSuccess = await AddModelAsync(model);

                if (!isSuccess)
                    return BadRequest("Failed to save entity in the database.");

                var id = GetModelId(model);
                SetDtoId(dto, id);

                return CreatedAtAction(actionName: "GetById", new { id }, dto);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="dto">The DTO containing updated data.</param>
        /// <returns>A status message indicating the result of the operation.</returns>
        /// <response code="200">If the entity was updated successfully.</response>
        /// <response code="400">If the ID is invalid or doesn't match the DTO ID.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] TDto dto)
        {
            if (!IsIdentifierIdentical(id, dto)) return BadRequest("Identifier mismatched");

            try
            {
                var model = _mapper.Map<TModel>(dto);
                var isSuccess = await UpdateModelAsync(model);

                return isSuccess
                    ? Ok($"Entity with ID: [{id}] successfully updated.")
                    : BadRequest("Failed to update entity in the database.");
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A status message indicating the result of the operation.</returns>
        /// <response code="200">If the entity was deleted successfully.</response>
        /// <response code="400">If the ID is invalid or entity doesn't exist.</response>
        /// <response code="500">If there was an internal server error or database restriction.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (id <= 0) return BadRequest($"Invalid Id: [{id}]");

            try
            {
                var isSuccess = await DeleteModelAsync(id);

                return isSuccess
                    ? Ok("Entity successfully deleted.")
                    : BadRequest($"Deletion failed: Entity with ID: [{id}] does not exist.");
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Deletion failed due to database restrictions."
                );
            }
        }

        #region Common Abstract Methods

        /// <summary>
        /// Gets the unique identifier from the model.
        /// </summary>
        /// <param name="model">The domain model instance.</param>
        /// <returns>The model's identifier.</returns>
        protected abstract int GetModelId(TModel model);

        /// <summary>
        /// Sets the identifier in the DTO.
        /// </summary>
        /// <param name="dto">The DTO instance.</param>
        /// <param name="id">The identifier to set.</param>
        protected abstract void SetDtoId(TDto dto, int id);

        /// <summary>
        /// Retrieves all models from the service.
        /// </summary>
        /// <returns>A collection of domain models.</returns>
        protected abstract Task<IEnumerable<TModel>> GetListOfModelsAsync();

        /// <summary>
        /// Retrieves a specific model by its ID.
        /// </summary>
        /// <param name="id">The ID of the model to retrieve.</param>
        /// <returns>The requested model or null if not found.</returns>
        protected abstract Task<TModel?> GetModelByIdAsync(int id);

        /// <summary>
        /// Adds a new model to the database.
        /// </summary>
        /// <param name="model">The model to add.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected abstract Task<bool> AddModelAsync(TModel model);

        /// <summary>
        /// Updates an existing model in the database.
        /// </summary>
        /// <param name="model">The model with updated data.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected abstract Task<bool> UpdateModelAsync(TModel model);

        /// <summary>
        /// Deletes a model from the database.
        /// </summary>
        /// <param name="id">The ID of the model to delete.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected abstract Task<bool> DeleteModelAsync(int id);

        /// <summary>
        /// Verifies that the ID matches the identifier in the DTO.
        /// </summary>
        /// <param name="id">The ID to verify.</param>
        /// <param name="dto">The DTO containing the identifier.</param>
        /// <returns>True if the identifiers match, otherwise false.</returns>
        protected abstract bool IsIdentifierIdentical(int id, TDto dto);

        #endregion
    }
}