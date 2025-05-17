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


        #region override abstract Methods
        protected override int GetModelId(UserModel model) => model.UserId;
        protected override void SetDtoId(UserDto dto, int id) => dto.UserId = id;
        protected override async Task<IEnumerable<UserModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<UserModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(UserModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(UserModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, UserDto dto) => id == dto.UserId;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}
