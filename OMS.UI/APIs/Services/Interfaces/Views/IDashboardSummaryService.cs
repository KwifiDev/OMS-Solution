using OMS.UI.Models.Views;

namespace OMS.UI.APIs.Services.Interfaces.Views
{
    /// <summary>
    /// Represents a service for retrieving Dashboard summary data.
    /// </summary>
    public interface IDashboardSummaryService
    {
        /// <summary>
        /// Retrieves one Dashboard records asynchronously.
        /// </summary>
        /// <returns>A  Dashboard Summary model.</returns>
        Task<DashboardSummaryModel?> GetData();
    }
}
