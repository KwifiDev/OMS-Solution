using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OMS.API.Dtos.Hybrid;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Hybrid;

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
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var model = _mapper.Map<FullRegisterModel>(dto);

                var isSuccess = await _authService.RegisterWithPersonAsync(model);

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseLoginDto>> SignInByUsernameAndPasswordAsync([FromBody] RequestLoginDto loginDto)
        {
            try
            {
                var model = await _authService.SignInAsync(_mapper.Map<RequestLoginModel>(loginDto));

                return model is null
                    ? NotFound()
                    : Ok(_mapper.Map<ResponseLoginDto>(model));
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
    }
}
