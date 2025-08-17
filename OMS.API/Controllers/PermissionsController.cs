using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

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

        #region override abstract Methods
        protected override int GetModelId(PermissionModel model) => model.Id;
        protected override void SetDtoId(PermissionDto dto, int id) => dto.Id = id;
        protected override async Task<IEnumerable<PermissionModel>> GetListOfModelsAsync() => await _service.GetAllAsync();
        protected override async Task<PermissionModel?> GetModelByIdAsync(int id) => await _service.GetByIdAsync(id);
        protected override async Task<bool> AddModelAsync(PermissionModel model) => await _service.AddAsync(model);
        protected override async Task<bool> UpdateModelAsync(PermissionModel model) => await _service.UpdateAsync(model);
        protected override async Task<bool> DeleteModelAsync(int id) => await _service.DeleteAsync(id);
        protected override bool IsIdentifierIdentical(int id, PermissionDto dto) => id == dto.Id;
        protected override async Task<bool> IsModelExistAsync(int id) => await _service.IsExistAsync(id);
        #endregion
    }
}

