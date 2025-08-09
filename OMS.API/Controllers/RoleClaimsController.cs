using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Hybrid;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.Common.Enums;
using System.Security.Claims;

namespace OMS.API.Controllers
{
    [Authorize]
    [Route("api/roleclaims")]
    [ApiController]
    public class RoleClaimsController : GenericViewController<IRoleClaimService, RoleClaimDto, RoleClaimModel>
    {
        public RoleClaimsController(IRoleClaimService service, IMapper mapper) : base(service, mapper)
        {
        }


        /// <summary>
        /// Retrieves all role Claims by Role Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/roleclaims/by-role/123
        ///     
        /// Returns all available role claims in the system.
        /// </remarks>
        /// <returns>List of all role claims by role</returns>
        /// <response code="200">Returns the complete list of role claims</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("by-role/{roleId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RoleClaimDto>>> GetRoleClaimsByRoleIdAsync([FromRoute] int roleId)
        {
            try
            {
                var models = await _service.GetRoleClaimsByRoleIdAsync(roleId);
                return Ok(_mapper.Map<IEnumerable<RoleClaimDto>>(models));
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error retrieving claims",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }


        /// <summary>
        /// add a role claim
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/roleclaims/add/123
        /// </remarks>
        /// <param name="roleId">The id of role</param>
        /// <param name="dto">The claim dto to add</param>
        /// <returns>The ok role claim added</returns>
        /// <response code="201">Returns ok</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("add/{roleId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRoleClaimsAsync([FromRoute] int roleId, [FromBody] ClaimDto dto)
        {
            try
            {
                var result = await _service.AddRoleClaimAsync(roleId, new Claim(dto.ClaimType, dto.ClaimValue));

                if (result != EnRoleResult.Success)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to save role claim in the database",
                        Errors = { { "General", new[] { "Failed to save role claim in the database" } } }
                    });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error creating role claims",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }


        /// <summary>
        /// remove a role claim
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/roleclaims/remove/123
        /// </remarks>
        /// <param name="roleId">The id of role</param>
        /// <param name="dto">The claim dto to remove</param>
        /// <returns>The ok role claim removed</returns>
        /// <response code="201">Returns ok</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("remove/{roleId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveRoleClaimsAsync([FromRoute] int roleId, [FromBody] ClaimDto dto)
        {
            try
            {
                var result = await _service.RemoveRoleClaimAsync(roleId, new Claim(dto.ClaimType, dto.ClaimValue));

                if (result != EnRoleResult.Success)
                {
                    return ValidationProblem(new ValidationProblemDetails
                    {
                        Title = "Validation Error",
                        Detail = "Failed to remove role claim in the database",
                        Errors = { { "General", new[] { "Failed to remove role claim in the database" } } }
                    });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(
                    title: "Error creating role claims",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");
            }
        }


        #region Common Abstract Methods
        protected override async Task<IEnumerable<RoleClaimModel>> GetListOfModelsAsync()
            => await _service.GetAllAsync();

        protected override Task<RoleClaimModel?> GetModelByIdAsync(int id)
            => _service.GetByIdAsync(id);
        #endregion
    }
}
