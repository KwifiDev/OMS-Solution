using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Hybrid;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing users data.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UsersController : GenericController<IUserService, UserDto, UserModel>
    {
        public UsersController(IUserService service, IMapper mapper) : base(service, mapper)
        {
        }


        /// <summary>
        /// Handles user login authentication (without token for now).
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/users/login
        ///     {
        ///         "username": "user123",
        ///         "password": "password123"
        ///     }
        /// </remarks>
        /// <param name="loginDto">The login credentials</param>
        /// <returns>User data if authenticated</returns>
        /// <response code="200">Returns the authenticated user's data</response>
        /// <response code="404">If username and password not Ok</response>
        /// <response code="500">On internal server error</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseLoginDto>> GetByUsernameAndPasswordAsync([FromBody] RequestLoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password)) return NotFound();
            
            try
            {
                var model = await _service.GetByUsernameAndPasswordAsync(loginDto.Username, loginDto.Password);
                
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
        /// Retrieves a specific entity by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/users/login/personid/123
        /// </remarks>
        /// <param name="personId">The ID of the entity to retrieve (must be positive integer)</param>
        /// <returns>The requested LoginDto</returns>
        /// <response code="200">Returns the requested entity</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("login/personid/{personId:int}")]
        [ActionName("GetUserLoginByPersonId")]
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
        /// Retrieves a specific entity by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/users/personid/123
        /// </remarks>
        /// <param name="personId">The person ID of the entity to retrieve (must be positive integer)</param>
        /// <returns>The requested userDto</returns>
        /// <response code="200">Returns the requested entity</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("personid/{personId:int}")]
        [ActionName("GetByPersonId")]
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
        /// Retrieves a specific entity by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/users/personid/123
        /// </remarks>
        /// <param name="personId">The person ID of the entity to retrieve (must be positive integer)</param>
        /// <returns>The requested userId</returns>
        /// <response code="200">Returns the requested entity</response>
        /// <response code="404">If entity was not found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("getid/personid/{personId:int}")]
        [ActionName("GetIdByPersonId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetIdByPersonIdAsync([FromRoute] int personId)
        {
            if (personId <= 0) return NotFound();

            try
            {
                int userId = await _service.GetIdByPersonIdAsync(personId);
                return userId is -1? NotFound() : Ok(userId);
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving UserId",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Gets the unique identifier from the UserModel.
        /// </summary>
        /// <param name="model">The UserModel instance.</param>
        /// <returns>The user's identifier.</returns>
        protected override int GetModelId(UserModel model) => model.UserId;

        /// <summary>
        /// Sets the identifier in the UserDto.
        /// </summary>
        /// <param name="dto">The UserDto instance.</param>
        /// <param name="id">The identifier to set.</param>
        protected override void SetDtoId(UserDto dto, int id) => dto.UserId = id;

        /// <summary>
        /// Retrieves all users from the service.
        /// </summary>
        /// <returns>A collection of UserModel instances.</returns>
        protected override async Task<IEnumerable<UserModel>> GetListOfModelsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The requested UserModel or null if not found.</returns>
        protected override async Task<UserModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="model">The UserModel to add.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> AddModelAsync(UserModel model) => await _service.AddAsync(model);

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="model">The UserModel with updated data.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> UpdateModelAsync(UserModel model) => await _service.UpdateAsync(model);

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);

        /// <summary>
        /// Verifies that the ID matches the user ID in the DTO.
        /// </summary>
        /// <param name="id">The ID to verify.</param>
        /// <param name="dto">The UserDto containing the user ID.</param>
        /// <returns>True if the IDs match, otherwise false.</returns>
        protected override bool IsIdentifierIdentical(int id, UserDto dto) => id == dto.UserId;

        /// <summary>
        /// Check a user from the database.
        /// </summary>
        /// <param name="id">The ID of the user to check if is exist.</param>
        /// <returns>True if the user exist, otherwise false.</returns>
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
    }
}
