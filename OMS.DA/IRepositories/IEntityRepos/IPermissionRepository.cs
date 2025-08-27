using OMS.DA.Entities;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
    }
}
