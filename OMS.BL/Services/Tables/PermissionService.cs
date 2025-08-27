using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PermissionService : GenericService<Permission, PermissionModel>, IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository ,IGenericRepository<Permission> repository, IMapperService mapper) : base(repository, mapper)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<PermissionModel>> GetAllAsync()
        {
            return _mapperService.Map<IEnumerable<Permission>, IEnumerable<PermissionModel>>(await _permissionRepository.GetAllAsync());
        }
    }
}
