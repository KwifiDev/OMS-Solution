using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
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
