using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class PermissionsConfigRepository : GenericRepository<PermissionsConfig>, IPermissionsConfigRepository
    {
        public PermissionsConfigRepository(AppDbContext context) : base(context)
        {

        }
    }
}
