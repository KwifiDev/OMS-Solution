using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Repositories.ViewRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public class ClientDetailRepository : GenericViewRepository<ClientDetail>, IGenericViewRepository<ClientDetail>, IClientDetailRepository
    {
        private readonly DbSet<ClientDetail> _clientDetails;

        public ClientDetailRepository(AppDbContext context) : base(context)
        {
            _clientDetails = context.Set<ClientDetail>();
        }

        public async Task<ClientDetail?> GetClientDetailByIdAsync(int clientId)
        {
            return await _clientDetails
                        .AsNoTracking()
                        .Where(m => m.ClientId == clientId)
                        .SingleOrDefaultAsync();
        }
    }
}
