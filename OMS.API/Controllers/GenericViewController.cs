using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.CustomAttributes;
using OMS.Common.Enums;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    /// <summary>
    /// Abstract base controller that provides common CRUD operations for API controllers.
    /// Important: Derived classes must add [ApiController] attribute.
    /// </summary>
    /// <typeparam name="TService">The service interface type used for business logic operations.</typeparam>
    /// <typeparam name="TDto">The Data Transfer Object (DTO) type used for API requests/responses.</typeparam>
    /// <typeparam name="TModel">The domain model type used for database operations.</typeparam>
    public abstract class GenericViewController<TService, TDto, TModel> : ControllerBase
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
        /// Initializes a new instance of the GenericViewController class.
        /// </summary>
        /// <param name="service">The service instance for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public GenericViewController(TService service, IMapper mapper)
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
        [AuthorizeCrud(EnCrudAction.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<PagedResult<TDto>>> GetPagedAsync([FromQuery] PaginationParams parameters)
        {
            try
            {
                var pagedResult = await GetListOfModelsAsync(parameters);

                return Ok(new PagedResult<TDto>
                {
                    Items = _mapper.Map<List<TDto>>(pagedResult.Items),
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
        [AuthorizeCrud(EnCrudAction.View)]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<TDto>> GetByIdAsync([FromRoute] int id)
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


        #region Common Abstract Methods

        /// <summary>
        /// Retrieves all models from the service.
        /// </summary>
        /// <returns>A collection of domain models.</returns>
        protected abstract Task<PagedResult<TModel>> GetListOfModelsAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves a specific model by its ID.
        /// </summary>
        /// <param name="id">The ID of the model to retrieve.</param>
        /// <returns>The requested model or null if not found.</returns>
        protected abstract Task<TModel?> GetModelByIdAsync(int id);

        #endregion
    }
}