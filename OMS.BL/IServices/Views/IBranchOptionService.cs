using OMS.BL.Dtos.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving branch option data.
    /// </summary>
    public interface IBranchOptionService
    {
        /// <summary>
        /// Retrieves all branch option records asynchronously.
        /// </summary>
        /// <returns>A collection of branch option models.</returns>
        Task<IEnumerable<BranchOptionDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a branch option record by its ID asynchronously.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The branch option model, or null if not found.</returns>
        Task<BranchOptionDto?> GetByIdAsync(int branchId);
    }
}
