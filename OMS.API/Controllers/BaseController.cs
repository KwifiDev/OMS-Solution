using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OMS.API.Controllers
{
    /// <summary>
    /// Abstract base controller that provides common CRUD operations for API controllers.
    /// Important: Derived classes must add [ApiController] attribute.
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
        /// <remarks>
        /// Sample request:
        ///     GET /api/entities
        ///     
        /// Returns all available entities in the system. Consider using filtering for large datasets.
        /// </remarks>
        /// <returns>List of all entities</returns>
        /// <response code="200">Returns the complete list of entities</response>
        /// <response code="500">If there was an internal server error</response>
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
                return Problem(
                    title: "Error retrieving entities",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }

        /// <summary>
        /// Retrieves a specific entity by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/entities/1
        /// </remarks>
        /// <param name="id">The ID of the entity to retrieve (must be positive integer)</param>
        /// <returns>The requested entity</returns>
        /// <response code="200">Returns the requested entity</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{id:int}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TDto>> GetByIdAsync([FromRoute] int id)
        {
            if (id <= 0) return NotFound();

            try
            {
                var model = await GetModelByIdAsync(id);
                return model is null
                    ? NotFound()
                    : Ok(_mapper.Map<TDto>(model));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving entity",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/entities
        ///     {
        ///         "name": "New Entity",
        ///         "description": "Entity description"
        ///     }
        /// </remarks>
        /// <param name="dto">The DTO containing data for the new entity</param>
        /// <returns>The created entity with generated ID</returns>
        /// <response code="201">Returns the newly created entity</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TDto>> AddAsync([FromBody] TDto dto)
        {
            try
            {
                var model = _mapper.Map<TModel>(dto);
                var isSuccess = await AddModelAsync(model);

                if (!isSuccess)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to save entity in the database",
                        Errors = { { "General", new[] { "Failed to save entity in the database" } } }
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
                    title: "Error creating entity",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/entities/1
        ///     {
        ///         "id": 1,
        ///         "name": "Updated Name",
        ///         "description": "Updated Description"
        ///     }
        ///
        /// Note: The ID in route must match the ID in request body.
        /// </remarks>
        /// <param name="id">The ID of the entity to update (must be positive integer and match ID in request body).</param>
        /// <param name="dto">The DTO containing updated data.</param>
        /// <returns>
        /// - 200 OK with updated entity if successful
        /// - 400 Bad Request if validation fails
        /// - 404 Not Found if entity doesn't exist
        /// - 500 Internal Server Error if unexpected error occurs
        /// </returns>
        /// <response code="204">Returns no content when entity updated</response>
        /// <response code="400">If ID is invalid, doesn't match DTO ID, or validation fails</response>
        /// <response code="404">If entity with specified ID doesn't exist</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] TDto dto)
        {
            if (id <= 0) return NotFound();

            try
            {
                if (!IsIdentifierIdentical(id, dto))
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Errors = { { "id", new[] { "Route ID must match body ID" } } }
                    });

                if (!await IsModelExistAsync(id)) return NotFound();

                var model = _mapper.Map<TModel>(dto);
                var isUpdated = await UpdateModelAsync(model);

                if (!isUpdated)
                    return Problem(
                        title: "Update failed",
                        detail: "Entity could not be updated",
                        statusCode: StatusCodes.Status400BadRequest
                    );

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Update operation failed",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                );
            }
        }

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/entities/1
        ///
        /// </remarks>
        /// <param name="id">The ID of the entity to delete (must be positive integer).</param>
        /// <returns>
        /// - 200 OK with boolean result (true if deleted successfully)
        /// - Appropriate error response for invalid requests
        /// </returns>
        /// <response code="200">Returns true if deletion was successful</response>
        /// <response code="404">If entity with specified ID doesn't exist</response>
        /// <response code="409">If conflict occurs (e.g. foreign key constraint)</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (id <= 0) return NotFound();

            try
            {
                if (!await IsModelExistAsync(id))
                    return NotFound();

                await DeleteModelAsync(id);
                return NoContent();
            }
            catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
            {
                return Problem(
                    detail: "Cannot delete entity due to existing relationships",
                    statusCode: StatusCodes.Status409Conflict
                );
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }


        /// <summary>
        /// Checks if an entity exists by its ID without retrieving its content.
        /// </summary>
        /// <remarks>
        /// This operation is more efficient than GET for existence checks as it doesn't return the entity body.
        /// 
        /// Example:
        /// HEAD /api/entities/123
        /// </remarks>
        /// <param name="id">The ID of the entity to check (must be positive integer).</param>
        /// <returns>
        /// - 200 OK with empty body if entity exists
        /// - 404 Not Found if entity doesn't exist
        /// - Appropriate error response for invalid requests
        /// </returns>
        /// <response code="200">Entity exists (returns empty response with headers)</response>
        /// <response code="404">If no entity exists with the specified ID</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpHead("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> HeadAsync([FromRoute] int id)
        {
            if (id <= 0) return NotFound();

            try
            {
                var isExist = await IsModelExistAsync(id);
                return isExist ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        private static bool IsForeignKeyViolation(DbUpdateException ex)
        {
            return ex.InnerException is SqlException sqlEx &&
                   (sqlEx.Number == 547 || sqlEx.Number == 2601);
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
        /// Retrieves a boolean value by its ID.
        /// </summary>
        /// <param name="id">The ID of the model.</param>
        /// <returns>True if the model exist, otherwise false.</returns>
        protected abstract Task<bool> IsModelExistAsync(int id);

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