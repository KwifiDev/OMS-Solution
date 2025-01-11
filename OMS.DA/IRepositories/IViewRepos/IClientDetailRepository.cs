using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IClientDetailRepository : IGenericViewRepository<ClientDetail>
    {
        /// <summary>
        /// Retrieves an ClientDetail by ClientId.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The ClientDetail if found; otherwise, null.</returns>
        Task<ClientDetail?> GetClientDetailByIdAsync(int clientId);
    }
}
