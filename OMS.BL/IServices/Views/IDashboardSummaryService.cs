using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a Dashboard for retrieving Dashboard Summary.
    /// </summary>
    public interface IDashboardSummaryService
    {
        /// <summary>
        /// Retrieves all Dashboard Summary asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of Dashboard Summary models.</returns>
        Task<DashboardSummaryModel?> GetData();
    }
}
