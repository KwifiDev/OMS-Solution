using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Hybrid;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Hybrid;
using OMS.Common.Data;
using OMS.Common.Enums;
using System.Security.Claims;

namespace OMS.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles user register
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/auth/register
        /// </remarks>
        /// <param name="dto">The Register dto</param>
        /// <returns>Id of New User</returns>
        /// <response code="200">Returns the user id</response>
        /// <response code="404">If person not data not Ok</response>
        /// <response code="500">On internal server error</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var model = _mapper.Map<FullRegisterModel>(dto);

                var isSuccess = await _authService.RegisterUserWithProfileAsync(model);

                if (!isSuccess)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to save User in the database",
                        Errors = { { "General", new[] { "Failed to save User in the database" } } }
                    });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error creating User or Person",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }



        /// <summary>
        /// Create user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/auth/
        /// </remarks>
        /// <param name="dto">The CreateUser dto</param>
        /// <returns>Id of New User</returns>
        /// <response code="200">Returns the user id</response>
        /// <response code="404">If person not data not Ok</response>
        /// <response code="500">On internal server error</response>
        [HttpPost()]
        [Authorize(Policy = PermissionsData.Users.Add)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreateAsync([FromBody] UserDto dto)
        {
            try
            {
                var model = _mapper.Map<RegisterModel>(dto);
                var isSuccess = await _authService.RegisterAsync(model);

                if (!isSuccess)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to Create User in the database",
                        Errors = { { "General", new[] { "Failed to Create User in the database" } } }
                    });
                }

                return Ok(model.UserId);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error creating User",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }


        /// <summary>
        /// Handles user login authentication (without token for now).
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/auth/login
        ///     {
        ///         "oldUsername": "user123",
        ///         "password": "password123"
        ///     }
        /// </remarks>
        /// <param name="loginDto">The login credentials</param>
        /// <returns>User data if authenticated</returns>
        /// <response code="200">Returns the authenticated user's data</response>
        /// <response code="404">If oldUsername and password not Ok</response>
        /// <response code="500">On internal server error</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginInfoDto>> SignInByUsernameAndPasswordAsync([FromBody] LoginDto loginDto)
        {
            try
            {
                var (TokenInfo, UserLogin, userClaims) = await _authService.LoginAsync(_mapper.Map<LoginModel>(loginDto));

                return UserLogin is null
                    ? NotFound()
                    : Ok(new LoginInfoDto { TokenInfo = TokenInfo, UserLogin = _mapper.Map<ResponseLoginDto>(UserLogin), Claims = userClaims });
            }
            catch (Exception ex)
            {
                return Problem(
                               title: "Login Failed",
                               detail: "An error occurred during login.",
                               instance: ex.Message,
                               statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Updates an user password.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/auth/changepassword
        ///
        /// </remarks>
        /// <param name="dto">The dto of the user password to update user password (must be positive integer of user Id).</param>
        /// <returns>
        /// - 200 OK with updated user activation if successful
        /// - 404 Not Found if user doesn't exist
        /// - 400 BadRequest dto not valid
        /// - 500 Internal Server Error if unexpected error occurs
        /// </returns>
        /// <response code="204">Returns no content when user password changed</response>
        /// <response code="404">If the password can,t changed</response>
        /// <response code="400">If ChangePasswordDto not valid</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("changepassword")]
        [Authorize(Policy = PermissionsData.Users.ChangePassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            try
            {
                var Model = _mapper.Map<ChangePasswordDto, ChangePasswordModel>(dto);

                bool isChanged = await _authService.ChangePasswordAsync(Model);

                return (isChanged) ? Ok() : NotFound();
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
        /// Updates my user password.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/auth/changemypassword
        ///
        /// </remarks>
        /// <param name="dto">The dto of the user password to update user password (must be positive integer of user Id).</param>
        /// <returns>
        /// - 200 OK with updated user activation if successful
        /// - 404 Not Found if user doesn't exist
        /// - 400 BadRequest dto not valid
        /// - 500 Internal Server Error if unexpected error occurs
        /// </returns>
        /// <response code="204">Returns no content when user password changed</response>
        /// <response code="404">If the password can,t changed</response>
        /// <response code="400">If ChangePasswordDto not valid</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("changemypassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeMyPassword([FromBody] ChangePasswordDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId is null || userId.Value != dto.UserId.ToString()) return Forbid();

            try
            {
                var Model = _mapper.Map<ChangePasswordDto, ChangePasswordModel>(dto);

                bool isChanged = await _authService.ChangePasswordAsync(Model);

                return (isChanged) ? Ok() : NotFound();
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
        /// Retrieves a specific user roles by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/auth/1
        /// </remarks>
        /// <param name="userId">The ID of the user to retrieve (must be positive integer)</param>
        /// <returns>The requested user roles</returns>
        /// <response code="200">Returns the requested user roles</response>
        /// <response code="204">Returns no user Roles</response>
        /// <response code="404">If roles was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("userroles/{userId:int}")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<IEnumerable<string>>> GetUserRolesByUserIdAsync([FromRoute] int userId)
        {
            if (userId <= 0) return NotFound();

            try
            {
                var userRoles = await _authService.GetUserRolesAsync(userId);
                return userRoles is null || !userRoles.Any()
                    ? NoContent()
                    : Ok(userRoles);
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
        /// add user to role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/auth/usertorole
        /// </remarks>
        /// <param name="dto">The dto contain userid and role name</param>
        /// <returns>Status 200 OK</returns>
        /// <response code="200">Returns ok if added</response>
        /// <response code="404">If person not data not Ok</response>
        /// <response code="500">On internal server error</response>
        [HttpPost("usertorole")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUserToRoleAsync([FromBody] UserRoleDto dto)
        {
            try
            {
                var model = _mapper.Map<UserRoleModel>(dto);
                var result = await _authService.AddUserToRoleAsync(model);

                return result switch
                {
                    EnAuthResult.UserNotFound => NotFound(),
                    EnAuthResult.RoleNotFound => NotFound(),
                    EnAuthResult.Conflict => Conflict(),
                    EnAuthResult.Success => Ok(),
                    _ => Problem(
                                 title: "Failed add user to role",
                                 detail: "Check database constraint",
                                 statusCode: StatusCodes.Status400BadRequest),
                };

            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Failed add User to role",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }


        /// <summary>
        /// remove user from role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/auth/userfromrole
        /// </remarks>
        /// <param name="dto">The dto contain userid and role name</param>
        /// <returns>Status 200 OK</returns>
        /// <response code="200">Returns ok if removed user from role</response>
        /// <response code="404">If person not data not Ok</response>
        /// <response code="500">On internal server error</response>
        [HttpPost("userfromrole")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveUserFromRoleAsync([FromBody] UserRoleDto dto)
        {
            try
            {
                var model = _mapper.Map<UserRoleModel>(dto);
                var result = await _authService.RemoveUserFromRoleAsync(model);

                return result switch
                {
                    EnAuthResult.UserNotFound => NotFound(),
                    EnAuthResult.RoleNotFound => NotFound(),
                    EnAuthResult.Conflict => Conflict(),
                    EnAuthResult.Success => Ok(),
                    _ => Problem(
                                 title: "Failed remove user from role",
                                 detail: "Check database constraint",
                                 statusCode: StatusCodes.Status400BadRequest),
                };

            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Failed remove User from role",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }


        /// <summary>
        /// change user roles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/auth/changeuserroles
        /// </remarks>
        /// <param name="dto">The dto contain userid and roles</param>
        /// <returns>Status 200 OK</returns>
        /// <response code="200">Returns ok if added</response>
        /// <response code="404">If person not data not Ok</response>
        /// <response code="500">On internal server error</response>
        [HttpPost("changeuserroles")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeUserRolesAsync([FromBody] InputUserRolesDto dto)
        {
            try
            {
                var model = _mapper.Map<InputUserRolesModel>(dto);
                var result = await _authService.ChangeUserRolesAsync(model);

                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Failed add User to role",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }


        /// <summary>
        /// check if user in role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/auth/userinrole
        /// </remarks>
        /// <param name="dto">The dto contain userid and role name</param>
        /// <returns>Status 200 OK</returns>
        /// <response code="200">Returns ok if user in role</response>
        /// <response code="404">If person not data not Ok</response>
        /// <response code="400">If user not found</response>
        /// <response code="500">On internal server error</response>
        [HttpPost("userinrole")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IsUserInRoleAsync([FromBody] UserRoleDto dto)
        {
            try
            {
                var model = _mapper.Map<UserRoleModel>(dto);
                var isInRole = await _authService.IsUserInRoleAsync(model);

                return isInRole switch
                {
                    null => BadRequest(),
                    false => NotFound(),
                    true => Ok()
                };

            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Failed check User in role",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }
    }
}
