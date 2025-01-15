using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IClientsByTypeRepository
    {
        /// <summary>
        /// Retrieves all ClientsByType.
        /// </summary>
        /// <returns>The task result contains the collection of ClientsByType.</returns>
        Task<IEnumerable<ClientsByType>> GetAllAsync();
    }
}
