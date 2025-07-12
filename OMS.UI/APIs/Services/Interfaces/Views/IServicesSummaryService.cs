using OMS.UI.Models;
using OMS.UI.Services.ModelTransfer;

namespace OMS.UI.APIs.Services.Interfaces.Views
{
    /// <summary>
    /// Represents a service for retrieving services summary data.
    /// </summary>
    public interface IServicesSummaryService : IDisplayService<ServicesSummaryModel>
    {
        /// <summary>
        /// Retrieves all service records asynchronously.
        /// </summary>
        /// <returns>A collection of Services Summary models.</returns>
        //Task<IEnumerable<ServicesSummaryModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a service record by its ID asynchronously.
        /// </summary>
        /// <param name="serviceId">The ID of the service.</param>
        /// <returns>The service summary model, or null if not found.</returns>
        //Task<ServicesSummaryModel?> GetByIdAsync(int serviceId);
    }
}
