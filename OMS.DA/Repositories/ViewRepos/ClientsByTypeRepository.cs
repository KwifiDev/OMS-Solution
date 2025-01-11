using OMS.DA.Context;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class ClientsByTypeRepository : GenericViewRepository<ClientsByType>, IGenericViewRepository<ClientsByType>
    {
        public ClientsByTypeRepository(AppDbContext context) : base(context)
        {

        }
    }
}
