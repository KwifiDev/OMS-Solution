using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PermissionsConfigService : GenericService<PermissionsConfig, PermissionsConfigDto>, IPermissionsConfigService
    {
        private readonly IPermissionsConfigRepository _permissionsConfigRepository;

        public PermissionsConfigService(IGenericRepository<PermissionsConfig> repo,
                                        IMapperService mapper,
                                        IPermissionsConfigRepository repository) : base(repo, mapper)
        {
            _permissionsConfigRepository = repository;
        }

        public override Task<bool> AddAsync(PermissionsConfigDto dto)
           => throw new NotSupportedException("Add operation is not supported for PermissionsConfigService.");
        public override Task<bool> UpdateAsync(PermissionsConfigDto dto)
            => throw new NotSupportedException("Update operation is not supported for PermissionsConfigService.");
        public override Task<bool> DeleteAsync(int id)
            => throw new NotSupportedException("Delete operation is not supported for PermissionsConfigService.");

        /*
              public async Task<IEnumerable<PermissionsConfigDto>> GetAllPermissionsConfigsAsync()
        {
            IEnumerable<PermissionsConfig> permissions = await _permissionsConfigRepository.GetAllAsync();

            return permissions?.Select(p => new PermissionsConfigDto
            {
                PermissionConfigId = p.PermissionConfigId,
                PermissionName = p.PermissionName,
                PermissionNo = p.PermissionNo

            }) ?? Enumerable.Empty<PermissionsConfigDto>();
        }

        public async Task<PermissionsConfigDto?> GetPermissionsConfigByIdAsync(int permissionsConfigId)
        {
            PermissionsConfig? permission = await _permissionsConfigRepository.GetByIdAsync(permissionsConfigId);

            return permission == null ? null : new PermissionsConfigDto
            {
                PermissionConfigId = permission.PermissionConfigId,
                PermissionName = permission.PermissionName,
                PermissionNo = permission.PermissionNo
            };
        }
         */

    }
}
