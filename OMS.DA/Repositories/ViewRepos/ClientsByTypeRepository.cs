using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class ClientsByTypeRepository : GenericViewRepository<ClientsByType>, IClientsByTypeRepository
    {
        public ClientsByTypeRepository(AppDbContext context) : base(context)
        {

        }
    }
}
