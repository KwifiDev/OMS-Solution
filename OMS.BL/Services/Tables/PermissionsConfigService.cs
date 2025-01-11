using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PermissionsConfigService : IPermissionsConfigService
    {
        private readonly IPermissionsConfigRepository _repository;

        public PermissionsConfigService(IPermissionsConfigRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PermissionsConfigModel>> GetAllPermissionsConfigsAsync()
        {
            IEnumerable<PermissionsConfig> permissions = await _repository.GetAllAsync();

            return permissions?.Select(p => new PermissionsConfigModel
            {
                PermissionConfigId = p.PermissionConfigId,
                PermissionName = p.PermissionName,
                PermissionNo = p.PermissionNo

            }) ?? Enumerable.Empty<PermissionsConfigModel>();
        }

        public async Task<PermissionsConfigModel?> GetPermissionsConfigByIdAsync(int permissionsConfigId)
        {
            PermissionsConfig? permission = await _repository.GetByIdAsync(permissionsConfigId);

            return permission == null ? null : new PermissionsConfigModel
            {
                PermissionConfigId = permission.PermissionConfigId,
                PermissionName = permission.PermissionName,
                PermissionNo = permission.PermissionNo
            };
        }

    }
}
