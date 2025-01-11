using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Context;
using OMS.DA.Entities;

namespace OMS.DA.Repositories.EntityRepos
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context)
        {

        }
    }
}
