using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class ClientDetailRepository : GenericViewRepository<ClientDetail>, IClientDetailRepository
    {
        private readonly DbSet<ClientDetail> _clientDetails;

        public ClientDetailRepository(AppDbContext context) : base(context)
        {
            _clientDetails = context.Set<ClientDetail>();
        }

    }
}
