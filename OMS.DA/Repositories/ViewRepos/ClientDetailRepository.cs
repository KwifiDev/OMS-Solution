using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class ClientDetailRepository : GenericViewRepository<ClientDetail>, IClientDetailRepository
    {
        public ClientDetailRepository(AppDbContext context) : base(context)
        {
        }

    }
}
