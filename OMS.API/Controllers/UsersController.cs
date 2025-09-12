using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.Common.Data;
using OMS.Common.Dtos.Hybrid;
using OMS.Common.Dtos.Tables;
using OMS.Common.Enums;
using OMS.Common.Extensions.Pagination;
using System.Security.Claims;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing users data.
    /// </summary>
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UsersController : GenericController<IUserService, UserDto, UserModel>
    {
        public UsersController(IUserService service, IMapper mapper) : base(service, mapper)
        {
        }


        public override Task<ActionResult<UserDto>> AddAsync([FromBody] UserDto dto)
            => Task.FromResult<ActionResult<UserDto>>(NotFound("This endpoint is disabled."));

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     PUT /api/users/1
        ///     
        /// Note: The ID in route must match the ID in request body.
        /// </remarks>
        /// <param name="id">The ID of the user to update (must be a positive integer and match ID in request body).</param>
        /// <param name="dto">The DTO containing updated user data.</param>
        /// <returns>No content if update is successful.</returns>
        /// <response code="204">Returns no content when user is successfully updated.</response>
        /// <response code="400">If ID is invalid, doesn't match DTO ID, or validation fails.</response>
        /// <response code="404">If user with specified ID doesn't exist.</response>
        /// <response code="409">If username is already used.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPut("{id:int}")]
        [Authorize(Policy = PermissionsData.Users.Edit)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public override async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UserDto dto)
        {
            if (id <= 0) return NotFound();

            try
            {
                if (!IsIdentifierIdentical(id, dto))
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Errors = { { "id", new[] { "Route ID must match body ID" } } }
                    });

                var result = await _service.UpdateUserAsync(_mapper.Map<UserModel>(dto));

                return result switch
                {
                    EnUserResult.NotFound => NotFound(),
                    EnUserResult.UserNameConflict => Conflict(),
                    EnUserResult.Success => NoContent(),
                    _ => Problem(
                                title: "Update failed",
                                detail: "Entity could not be updated",
                                statusCode: StatusCodes.Status400BadRequest),
                };
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
        /// Updates the current authenticated user's information.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     PUT /api/users/updatemyuser/1
        ///     
        /// Note: The ID in route must match the ID in request body and the authenticated user's ID.
        /// </remarks>
        /// <param name="id">The ID of the user to update (must be a positive integer and match authenticated user's ID).</param>
        /// <param name="dto">The DTO containing updated user data.</param>
        /// <returns>No content if update is successful.</returns>
        /// <response code="204">Returns no content when user is successfully updated.</response>
        /// <response code="400">If ID is invalid, doesn't match DTO ID, or validation fails.</response>
        /// <response code="403">If user is not authorized to update this account.</response>
        /// <response code="404">If user with specified ID doesn't exist.</response>
        /// <response code="409">If username is already used.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPut("updatemyuser/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMyUserAsync([FromRoute] int id, [FromBody] UserDto dto)
        {
            if (id <= 0) return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId is null || userId.Value != dto.Id.ToString()) return Forbid();

            try
            {
                if (!IsIdentifierIdentical(id, dto))
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Errors = { { "id", new[] { "Route ID must match body ID" } } }
                    });

                var result = await _service.UpdateUserAsync(_mapper.Map<UserModel>(dto));

                return result switch
                {
                    EnUserResult.NotFound => NotFound(),
                    EnUserResult.UserNameConflict => Conflict(),
                    EnUserResult.Success => NoContent(),
                    _ => Problem(
                                title: "Update failed",
                                detail: "Entity could not be updated",
                                statusCode: StatusCodes.Status400BadRequest),
                };
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
        /// Retrieves login information for a specific user by person ID.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     GET /api/users/123/login
        /// </remarks>
        /// <param name="personId">The person ID of the user to retrieve login information for (must be a positive integer).</param>
        /// <returns>Login information for the specified user.</returns>
        /// <response code="200">Returns the requested login information.</response>
        /// <response code="404">If user with specified person ID doesn't exist.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("{personId:int}/login")]
        [Authorize(Policy = PermissionsData.Users.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResultLoginDto>> GetUserLoginByPersonIdAsync([FromRoute] int personId)
        {
            if (personId <= 0) return NotFound();

            try
            {
                var model = await _service.GetUserLoginByPersonIdAsync(personId);
                return model is null
                    ? NotFound()
                    : Ok(_mapper.Map<ResultLoginDto>(model));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving UserLogin",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieves a user by their person ID.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     GET /api/users/123/personid
        /// </remarks>
        /// <param name="personId">The person ID of the user to retrieve (must be a positive integer).</param>
        /// <returns>The requested user information.</returns>
        /// <response code="200">Returns the requested user information.</response>
        /// <response code="404">If user with specified person ID doesn't exist.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("{personId:int}/personid")]
        [Authorize(Policy = PermissionsData.Users.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetByPersonIdAsync([FromRoute] int personId)
        {
            if (personId <= 0) return NotFound();

            try
            {
                var model = await _service.GetByPersonIdAsync(personId);
                return model is null
                    ? NotFound()
                    : Ok(_mapper.Map<UserDto>(model));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving User",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieves a user ID by person ID.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     GET /api/users/123/id
        /// </remarks>
        /// <param name="personId">The person ID to retrieve the user ID for (must be a positive integer).</param>
        /// <returns>The user ID associated with the specified person ID.</returns>
        /// <response code="200">Returns the requested user ID.</response>
        /// <response code="404">If no user exists with the specified person ID.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("{personId:int}/id")]
        [Authorize(Policy = PermissionsData.Users.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetIdByPersonIdAsync([FromRoute] int personId)
        {
            if (personId <= 0) return NotFound();

            try
            {
                int userId = await _service.GetIdByPersonIdAsync(personId);
                return userId is -1 ? NotFound() : Ok(userId);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving Id",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Checks if a user exists and is active by user ID.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     HEAD /api/users/123/isactive
        ///     
        /// This operation is efficient for existence checks as it doesn't return the user body.
        /// </remarks>
        /// <param name="userId">The user ID to check for active status (must be a positive integer).</param>
        /// <returns>Empty response indicating active status.</returns>
        /// <response code="200">User exists and is active.</response>
        /// <response code="404">If user doesn't exist or is not active.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpHead("{userId:int}/isactive")]
        [Authorize(Policy = PermissionsData.Users.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IsUserActive([FromRoute] int userId)
        {
            if (userId <= 0) return NotFound();

            try
            {
                var isExistAndActive = await _service.IsUserActive(userId);
                return (isExistAndActive) ? Ok() : NotFound();
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
        /// Updates the activation status of a user.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     PUT /api/users/updateactivation
        ///     
        /// Request body should contain UserActivationDto with userId and isActive properties.
        /// </remarks>
        /// <param name="userActivationDto">The DTO containing user ID and activation status to update.</param>
        /// <returns>No content if update is successful.</returns>
        /// <response code="204">Returns no content when activation status is successfully updated.</response>
        /// <response code="404">If user with specified ID doesn't exist.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPut("updateactivation")]
        [Authorize(Policy = PermissionsData.Users.Activation)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserActivationAsync([FromBody] UserActivationDto userActivationDto)
        {
            if (userActivationDto.UserId <= 0) return NotFound();

            try
            {
                bool isSuccess = await _service.UpdateUserActivationStatus(userActivationDto.UserId, userActivationDto.IsActive);
                return (isSuccess) ? NoContent() : NotFound();
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
        /// Checks if a username is available for use.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     POST /api/users/checkusernameavailable
        ///     
        /// Request body should contain UsernameAvailableDto with userId and username properties.
        /// </remarks>
        /// <param name="dto">The DTO containing user ID and username to check for availability.</param>
        /// <returns>Empty response if username is available.</returns>
        /// <response code="200">Username is available.</response>
        /// <response code="400">If username is not valid.</response>
        /// <response code="409">If username is already used.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost("checkusernameavailable")]
        [Authorize(Policy = PermissionsData.Users.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckUsernameAvailable([FromBody] UsernameAvailableDto dto)
        {
            try
            {
                var isUsernameUsed = await _service.IsUsernameUsedAsync(dto.UserId, dto.Username);
                return (!isUsernameUsed) ? Ok() : Conflict();
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
        protected override int GetModelId(UserModel model) => model.Id;
        protected override void SetDtoId(UserDto dto, int id) => dto.Id = id;
        protected override async Task<PagedResult<UserModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<UserModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(UserModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(UserModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, UserDto dto) => id == dto.Id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}
