﻿using OMS.BL.Dtos.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving clients grouped by type.
    /// </summary>
    public interface IClientsByTypeService
    {
        /// <summary>
        /// Retrieves all clients grouped by type asynchronously.
        /// </summary>
        /// <returns>A collection of clients grouped by type.</returns>
        Task<IEnumerable<ClientsByTypeDto>> GetAllAsync();
    }
}
