using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.Common.Data;
using OMS.Common.Dtos.Tables;
using OMS.Common.Extensions.Pagination;

namespace OMS.API.Controllers
{
    /// <summary>
    /// API controller for managing permissions data.
    /// </summary>
    [Authorize]
    [Route("api/permissions")]
    [ApiController]
    public class PermissionsController : GenericController<IPermissionService, PermissionDto, PermissionModel>
    {
        /// <summary>
        /// Initializes a new instance of the PermissionsController class.
        /// </summary>
        /// <param name="permissionService">The permission service for business logic operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public PermissionsController(IPermissionService permissionService, IMapper mapper)
            : base(permissionService, mapper)
        {
        }

        /// <summary>
        /// Retrieves all permissions.
        /// </summary>
        /// <remarks>
        /// Example request:
        ///     GET /api/permissions/all
        /// Returns all available permissions in the system.
        /// </remarks>
        /// <returns>List of all permissions.</returns>
        /// <response code="200">Returns the complete list of permissions.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpGet("all")]
        [Authorize(Policy = PermissionsData.Permissions.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAllAsync()
        {
            try
            {
                var items = await _service.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<PermissionModel>, IEnumerable<PermissionDto>>(items));
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

        #region override abstract Methods
        protected override int GetModelId(PermissionModel model) => model.Id;
        protected override void SetDtoId(PermissionDto dto, int id) => dto.Id = id;
        protected override async Task<PagedResult<PermissionModel>> GetListOfModelsAsync(PaginationParams parameters) => await _service.GetPagedAsync(parameters);
        protected override async Task<PermissionModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(PermissionModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(PermissionModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, PermissionDto dto) => id == dto.Id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}

