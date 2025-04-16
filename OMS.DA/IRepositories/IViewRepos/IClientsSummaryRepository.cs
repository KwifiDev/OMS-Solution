using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IClientsSummaryRepository
    {
        /// <summary>
        /// Retrieves all ClientSummary
        /// </summary>
        /// <returns>The task result contains the collection of ClientSummary.</returns>
        Task<IEnumerable<ClientsSummary>> GetAllAsync();

        /// <summary>
        /// Retrieves an ClientSummary by ClientId.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The ClientSummary if found; otherwise, null.</returns>
        Task<ClientsSummary?> GetByIdAsync(int clientId);
    }
}
