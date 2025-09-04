using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Hybrid;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.Common.Data;
using OMS.Common.Enums;
using OMS.Common.Extensions.Pagination;
using System.IdentityModel.Tokens.Jwt;
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
        /// Sample request:
        ///
        ///     PUT /api/users/1
        ///     
        /// Note: The ID in route must match the ID in request body.
        /// </remarks>
        /// <param name="id">The ID of the user to update (must be positive integer and match ID in request body).</param>
        /// <param name="dto">The DTO containing updated data.</param>
        /// <returns>
        /// - 200 OK with updated user if successful
        /// - 400 Bad Request if validation fails
        /// - 404 Not Found if user doesn't exist
        /// - 409 already used oldUsername
        /// - 500 Internal Server Error if unexpected error occurs
        /// </returns>
        /// <response code="204">Returns no content when user updated</response>
        /// <response code="400">If ID is invalid, doesn't match DTO ID, or validation fails</response>
        /// <response code="404">If user with specified ID doesn't exist</response>
        /// <response code="409">If user with specified oldUsername exist</response>
        /// <response code="500">If there was an internal server error</response>
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
        /// Updates an existing user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/users/1
        ///     
        /// Note: The ID in route must match the ID in request body.
        /// </remarks>
        /// <param name="id">The ID of the user to update (must be positive integer and match ID in request body).</param>
        /// <param name="dto">The DTO containing updated data.</param>
        /// <returns>
        /// - 200 OK with updated user if successful
        /// - 400 Bad Request if validation fails
        /// - 404 Not Found if user doesn't exist
        /// - 409 already used oldUsername
        /// - 500 Internal Server Error if unexpected error occurs
        /// </returns>
        /// <response code="204">Returns no content when user updated</response>
        /// <response code="400">If ID is invalid, doesn't match DTO ID, or validation fails</response>
        /// <response code="404">If user with specified ID doesn't exist</response>
        /// <response code="409">If user with specified oldUsername exist</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("updatemyuser/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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
        /// Retrieves a specific login entity by person ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/users/123/login
        ///     123 is the person ID
        /// </remarks>
        /// <param name="personId">The person ID of the entity to retrieve login info (must be positive integer)</param>
        /// <returns>The requested LoginDto</returns>
        /// <response code="200">Returns the requested login info</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{personId:int}/login")]
        [Authorize(Policy = PermissionsData.Users.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseLoginDto>> GetUserLoginByPersonIdAsync([FromRoute] int personId)
        {
            if (personId <= 0) return NotFound();

            try
            {
                var model = await _service.GetUserLoginByPersonIdAsync(personId);
                return model is null
                    ? NotFound()
                    : Ok(_mapper.Map<ResponseLoginDto>(model));
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
        /// Retrieves a specific User by person ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/users/123
        /// </remarks>
        /// <param name="personId">The person ID of the entity to retrieve user dto (must be positive integer)</param>
        /// <returns>The requested userDto</returns>
        /// <response code="200">Returns the requested user</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If there was an internal server error</response>
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
        /// Retrieves a specific user ID by person ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/users/123/id
        /// </remarks>
        /// <param name="personId">The person ID of the entity to retrieve user ID (must be positive integer)</param>
        /// <returns>The requested userId</returns>
        /// <response code="200">Returns the requested user id</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If there was an internal server error</response>
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
        /// Checks if an user exists and active by its ID without retrieving its content.
        /// </summary>
        /// <remarks>
        /// This operation is more efficient than GET for existence checks as it doesn't return the user body.
        /// 
        /// Example:
        /// HEAD /api/users/123/isactive
        /// </remarks>
        /// <param name="userId">The user ID of the user to check if is active (must be positive integer).</param>
        /// <returns>
        /// - 200 OK with empty body if user is active
        /// - 404 Not Found if user doesn't exist or not Active
        /// - Appropriate error response for invalid requests
        /// </returns>
        /// <response code="200">user exists and active (returns empty response with headers)</response>
        /// <response code="404">If no user exists or not active user with the specified ID</response>
        /// <response code="500">If there was an internal server error</response>
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
        /// Updates an existing users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/users/1
        ///
        /// </remarks>
        /// <param name="dto">The dto of the user Activation to update user activate (must be positive integer of user Id).</param>
        /// <returns>
        /// - 200 OK with updated user activation if successful
        /// - 404 Not Found if user doesn't exist
        /// - 500 Internal Server Error if unexpected error occurs
        /// </returns>
        /// <response code="204">Returns no content when user updated</response>
        /// <response code="404">If user with specified ID doesn't exist</response>
        /// <response code="500">If there was an internal server error</response>
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

                return (isSuccess) ? Ok() : NotFound();
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
        /// Checks if an username available.
        /// </summary>
        /// <remarks>
        /// This operation is more efficient than GET for existence checks as it doesn't return the user body.
        /// 
        /// Example:
        /// HEAD /api/users/username/checkusernameavailable
        /// </remarks>
        /// <param name="username">The username of the user to check if is used.</param>
        /// <returns>
        /// - 200 OK with empty body if username available
        /// - Appropriate error response for invalid requests
        /// </returns>
        /// <response code="200">username available (returns empty response with headers)</response>
        /// <response code="404">If username not valied</response>
        /// <response code="409">If username used</response>
        /// <response code="500">If there was an internal server error</response>
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
