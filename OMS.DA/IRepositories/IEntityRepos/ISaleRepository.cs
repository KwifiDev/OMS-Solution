using OMS.Common.Enums;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface ISaleRepository
    {
        /// <summary>
        /// Create a sale record with fully computed columns then insert it into sales table.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="serviceId">The ID of the client.</param>
        /// <param name="quantity">The quantity of service.</param>
        /// <param name="description">Optional description for the sale.</param>
        /// <param name="notes">Optional notes for the sale.</param>
        /// <param name="status">Sale Status for the sale.</param>
        /// <param name="createdByUserId">The ID of the user who created the sale.</param>
        /// <returns>The new sale id of the sale or -1 if an error or 0 if (clientId,serviceId,userId) not correct.</returns>
        Task<int> AddSaleAsync(int clientId, int serviceId, short quantity, string? description, string? notes, EnSaleStatus status, int createdByUserId);
    }
}
