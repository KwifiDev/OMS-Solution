using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class ClientsByTypeRepository : GenericViewRepository<ClientsByType>, IClientsByTypeRepository
    {
        public ClientsByTypeRepository(AppDbContext context) : base(context)
        {

        }
    }
}
