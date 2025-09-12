using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Extensions;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Hybrid;
using OMS.Common.Data;
using OMS.Common.Dtos.Hybrid;
using OMS.Common.Dtos.Tables;
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
        /// Registers a new user with profile information.
        /// </summary>
        /// <remarks>
        /// <code>
        /// POST /api/auth/register
        /// </code>
        /// </remarks>
        /// <param name="dto">The registration data transfer object.</param>
        /// <returns>Returns 200 OK if registration succeeded, otherwise validation or error details.</returns>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">Validation error in the request data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// Creates a new user.
        /// </summary>
        /// <remarks>
        /// <code>
        /// POST /api/auth
        /// </code>
        /// </remarks>
        /// <param name="dto">The user creation data transfer object.</param>
        /// <returns>Returns the new user's ID if successful.</returns>
        /// <response code="200">User created successfully, returns user ID.</response>
        /// <response code="400">Validation error in the request data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost()]
        [Authorize(Policy = PermissionsData.Users.Add)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

                return Ok(model.Id);
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
        /// Authenticates a user by username and password.
        /// </summary>
        /// <remarks>
        /// <code>
        /// POST /api/auth/login
        /// {
        ///   "username": "user123",
        ///   "password": "password123"
        /// }
        /// </code>
        /// </remarks>
        /// <param name="loginDto">The login credentials.</param>
        /// <returns>Returns user data and token if authentication is successful.</returns>
        /// <response code="200">Authentication successful, returns user data and token.</response>
        /// <response code="400">Validation error in the request data.</response>
        /// <response code="404">Invalid username or password.</response>
        /// <response code="500">Internal server error.</response>
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
                    : Ok(new LoginInfoDto { TokenInfo = _mapper.Map<TokenDto>(TokenInfo), UserLogin = _mapper.Map<ResultLoginDto>(UserLogin), Claims = userClaims });
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
        /// Generates a new token for the specified user.
        /// </summary>
        /// <remarks>
        /// <code>
        /// GET /api/auth/refreshtoken/1
        /// </code>
        /// </remarks>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>Returns a new token if successful.</returns>
        /// <response code="200">Token generated successfully.</response>
        /// <response code="400">Invalid user ID.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("refreshtoken/{userId:int}")]
        [Authorize(PermissionsData.Users.ManageRoles)]
        [ServiceFilter(typeof(ClearPermissionCacheFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TokenModel>> RefreshTokenAsync([FromRoute] int userId)
        {
            try
            {
                var TokenInfo = await _authService.UpdateToken(userId);

                return TokenInfo is null ? NotFound() : Ok(TokenInfo);
            }
            catch (Exception ex)
            {
                return Problem(
                               title: "Generate Token Failed",
                               detail: "An error occurred during Generate Token.",
                               instance: ex.Message,
                               statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <remarks>
        /// <code>
        /// PUT /api/auth/changepassword
        /// </code>
        /// </remarks>
        /// <param name="dto">The change password data transfer object.</param>
        /// <returns>Returns 200 OK if password changed successfully.</returns>
        /// <response code="200">Password changed successfully.</response>
        /// <response code="400">Validation error in the request data.</response>
        /// <response code="404">User not found or password change failed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("changepassword")]
        [Authorize(Policy = PermissionsData.Users.ChangePassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Changes the current user's password.
        /// </summary>
        /// <remarks>
        /// <code>
        /// PUT /api/auth/changemypassword
        /// </code>
        /// </remarks>
        /// <param name="dto">The change password data transfer object.</param>
        /// <returns>Returns 200 OK if password changed successfully.</returns>
        /// <response code="200">Password changed successfully.</response>
        /// <response code="400">Validation error in the request data or user not authorized.</response>
        /// <response code="404">User not found or password change failed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("changemypassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Retrieves the roles assigned to a specific user.
        /// </summary>
        /// <remarks>
        /// <code>
        /// GET /api/auth/userroles/1
        /// </code>
        /// </remarks>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>Returns a list of user roles.</returns>
        /// <response code="200">Roles found and returned.</response>
        /// <response code="204">No roles assigned to the user.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("userroles/{userId:int}")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRolesByUserIdAsync([FromRoute] int userId)
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
        /// Retrieves the claims assigned to a specific user.
        /// </summary>
        /// <remarks>
        /// <code>
        /// GET /api/auth/userclaims/1
        /// </code>
        /// </remarks>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>Returns a list of user claims.</returns>
        /// <response code="200">Claims found and returned.</response>
        /// <response code="204">No claims assigned to the user.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("userclaims/{userId:int}")]
        [Authorize(Policy = PermissionsData.Roles.View)]
        [ServiceFilter(typeof(ClearPermissionCacheFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<string>>> GetUserClaimsByUserIdAsync([FromRoute] int userId)
        {
            if (userId <= 0) return NotFound();

            try
            {
                var userClaims = await _authService.GetUserClaimsAsync(userId);
                return userClaims is null || !userClaims.Any()
                    ? NoContent()
                    : Ok(userClaims);
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
        /// Adds a user to a role.
        /// </summary>
        /// <remarks>
        /// <code>
        /// POST /api/auth/usertorole
        /// </code>
        /// </remarks>
        /// <param name="dto">The user-role data transfer object.</param>
        /// <returns>Returns 200 OK if added successfully.</returns>
        /// <response code="200">User added to role successfully.</response>
        /// <response code="400">Validation error or database constraint error.</response>
        /// <response code="404">User or role not found.</response>
        /// <response code="409">Conflict (user already in role).</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("usertorole")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUserToRoleAsync([FromBody] UserRoleDto dto)
        {
            if (dto.UserId <= 0) return NotFound();

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
        /// Removes a user from a role.
        /// </summary>
        /// <remarks>
        /// <code>
        /// POST /api/auth/userfromrole
        /// </code>
        /// </remarks>
        /// <param name="dto">The user-role data transfer object.</param>
        /// <returns>Returns 200 OK if removed successfully.</returns>
        /// <response code="200">User removed from role successfully.</response>
        /// <response code="400">Validation error or database constraint error.</response>
        /// <response code="404">User or role not found.</response>
        /// <response code="409">Conflict (user not in role).</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("userfromrole")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveUserFromRoleAsync([FromBody] UserRoleDto dto)
        {
            if (dto.UserId <= 0) return NotFound();

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
        /// Changes the roles assigned to a user.
        /// </summary>
        /// <remarks>
        /// <code>
        /// POST /api/auth/changeuserroles
        /// </code>
        /// </remarks>
        /// <param name="dto">The input user roles data transfer object.</param>
        /// <returns>Returns 200 OK if roles changed successfully.</returns>
        /// <response code="200">User roles changed successfully.</response>
        /// <response code="400">Validation error in the request data.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("changeuserroles")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeUserRolesAsync([FromBody] InputUserRolesDto dto)
        {
            if (dto.UserId <= 0) return NotFound();

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
        /// Checks if a user is in a specific role.
        /// </summary>
        /// <remarks>
        /// <code>
        /// POST /api/auth/userinrole
        /// </code>
        /// </remarks>
        /// <param name="dto">The user-role data transfer object.</param>
        /// <returns>Returns 200 OK if user is in role, 404 if not, 400 if user not found.</returns>
        /// <response code="200">User is in the specified role.</response>
        /// <response code="400">Validation error or user not found.</response>
        /// <response code="404">User is not in the specified role.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("userinrole")]
        [Authorize(Policy = PermissionsData.Users.ManageRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IsUserInRoleAsync([FromBody] UserRoleDto dto)
        {
            if (dto.UserId <= 0) return NotFound();

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
