using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PermissionService : GenericService<Permission, PermissionModel>, IPermissionService
    {
        public PermissionService(IGenericRepository<Permission> repository, IMapperService mapper) : base(repository, mapper)
        {
        }
    }
}
