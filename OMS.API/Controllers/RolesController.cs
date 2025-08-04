using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.Common.Enums;

namespace OMS.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/roles
        ///     
        /// Returns all available roles in the system. Consider using filtering for large datasets.
        /// </remarks>
        /// <returns>List of all roles</returns>
        /// <response code="200">Returns the complete list of roles</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllAsync()
        {
            try
            {
                var models = await _roleService.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<RoleDto>>(models));
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
        /// Retrieves a specific role by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/roles/1
        /// </remarks>
        /// <param name="id">The ID of the role to retrieve (must be positive integer)</param>
        /// <returns>The requested role</returns>
        /// <response code="200">Returns the requested role</response>
        /// <response code="404">If role was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{id:int}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleDto>> GetByIdAsync([FromRoute] int id)
        {
            if (id <= 0) return NotFound();

            try
            {
                var model = await _roleService.FindByIdAsync(id);
                return model is null
                    ? NotFound()
                    : Ok(_mapper.Map<RoleDto>(model));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving role",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieves a specific role by its name.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/roles/roleName
        /// </remarks>
        /// <param name="roleName">The Name of the role to retrieve</param>
        /// <returns>The requested role</returns>
        /// <response code="200">Returns the requested role</response>
        /// <response code="404">If role was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{roleName}")]
        [ActionName("GetByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleDto>> GetByNameAsync([FromRoute] string roleName)
        {
            try
            {
                var model = await _roleService.FindByNameAsync(roleName);
                return model is null
                    ? NotFound()
                    : Ok(_mapper.Map<RoleDto>(model));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving role",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/roles
        /// </remarks>
        /// <param name="roleDto">The DTO containing data for the new role</param>
        /// <returns>The created role with generated ID</returns>
        /// <response code="201">Returns the newly created role</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleDto>> AddAsync([FromBody] RoleDto roleDto)
        {
            try
            {
                var model = _mapper.Map<RoleModel>(roleDto);
                var result = await _roleService.AddAsync(model);

                if (result != EnRoleResult.Success)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to save role in the database",
                        Errors = { { "General", new[] { "Failed to save role in the database" } } }
                    });
                }

                roleDto.Id = model.Id;

                return CreatedAtAction(
                    actionName: "GetById",
                    routeValues: new { id = roleDto.Id },
                    value: roleDto);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error creating role",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/roles/1
        ///
        /// Note: The ID in route must match the ID in request body.
        /// </remarks>
        /// <param name="id">The ID of the role to update (must be positive integer and match ID in request body).</param>
        /// <param name="dto">The DTO containing updated data.</param>
        /// <returns>
        /// - 200 OK with updated role if successful
        /// - 400 Bad Request if validation fails
        /// - 404 Not Found if role doesn't exist
        /// - 500 Internal Server Error if unexpected error occurs
        /// </returns>
        /// <response code="204">Returns no content when role updated</response>
        /// <response code="400">If ID is invalid, doesn't match DTO ID, or validation fails</response>
        /// <response code="404">If role with specified ID doesn't exist</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] RoleDto dto)
        {
            if (id <= 0) return NotFound();

            try
            {
                if (id != dto.Id)
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Errors = { { "id", new[] { "Route ID must match body ID" } } }
                    });

                if (!await _roleService.IsExists(id)) return NotFound();

                var model = _mapper.Map<RoleModel>(dto);
                var result = await _roleService.UpdateAsync(model);

                if (result != EnRoleResult.Success)
                    return Problem(
                        title: "Update failed",
                        detail: "role could not be updated",
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
        /// Deletes an role by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/roles/1
        ///
        /// </remarks>
        /// <param name="id">The ID of the role to delete (must be positive integer).</param>
        /// <returns>
        /// - 200 OK with boolean result (true if deleted successfully)
        /// - Appropriate error response for invalid requests
        /// </returns>
        /// <response code="200">Returns true if deletion was successful</response>
        /// <response code="404">If role with specified ID doesn't exist</response>
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
                if (!await _roleService.IsExists(id))
                    return NotFound();

                var result = await _roleService.DeleteAsync(id);

                return result == EnRoleResult.Success ? NoContent() : BadRequest();
            }
            catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
            {
                return Problem(
                    detail: "Cannot delete role due to existing relationships",
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
        /// Checks if an role exists by its ID without retrieving its content.
        /// </summary>
        /// <remarks>
        /// This operation is more efficient than GET for existence checks as it doesn't return the role body.
        /// 
        /// Example:
        /// HEAD /api/entities/123
        /// </remarks>
        /// <param name="id">The ID of the role to check (must be positive integer).</param>
        /// <returns>
        /// - 200 OK with empty body if role exists
        /// - 404 Not Found if role doesn't exist
        /// - Appropriate error response for invalid requests
        /// </returns>
        /// <response code="200">Role exists (returns empty response with headers)</response>
        /// <response code="404">If no role exists with the specified ID</response>
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
                var isExist = await _roleService.IsExists(id);
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

    }
}
