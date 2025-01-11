using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Context;
using OMS.DA.Entities;

namespace OMS.DA.Repositories.EntityRepos
{
    public class PermissionsConfigRepository : GenericRepository<PermissionsConfig>, IPermissionsConfigRepository
    {
        public PermissionsConfigRepository(AppDbContext context) : base(context)
        {

        }
    }
}
