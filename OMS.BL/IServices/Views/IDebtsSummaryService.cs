﻿using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for managing debts summary.
    /// </summary>
    public interface IDebtsSummaryService
    {
        /// <summary>
        /// Retrieves all debts summary asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of debts summary models.</returns>
        Task<IEnumerable<DebtsSummaryModel>> GetAllDebtsSummaryAsync();

        /// <summary>
        /// Retrieves a debt summary by its ID asynchronously.
        /// </summary>
        /// <param name="debtId">The ID of the debt summary to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the debt summary model, or null if not found.</returns>
        Task<DebtsSummaryModel?> GetDebtSummaryByIdAsync(int debtId);
    }
}
