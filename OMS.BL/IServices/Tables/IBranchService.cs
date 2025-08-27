using OMS.BL.Models.Tables;
using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Tables
{
    /// <summary>
    /// Represents the interface for managing branches.
    /// </summary>
    public interface IBranchService
    {
        /// <summary>
        /// Retrieves all branches asynchronously.
        /// </summary>
        /// <returns>A collection of branch models.</returns>
        Task<PagedResult<BranchModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves a branch by its ID asynchronously.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The branch model if found, otherwise null.</returns>
        Task<BranchModel?> GetByIdAsync(int branchId);

        /// <summary>
        /// find a branch by their ID asynchronously.
        /// </summary>
        /// <param name="branchId">The ID of the branch to find.</param>
        /// <returns>True if the branch was exist, otherwise false.</returns>
        Task<bool> IsExistAsync(int branchId);

        /// <summary>
        /// Adds a new branch asynchronously.
        /// </summary>
        /// <param name="model">The branch model to add.</param>
        /// <returns>True if the branch was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(BranchModel model);

        /// <summary>
        /// Updates an existing branch asynchronously.
        /// </summary>
        /// <param name="model">The branch model to update.</param>
        /// <returns>True if the branch was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(BranchModel model);

        /// <summary>
        /// Deletes a branch by its ID asynchronously.
        /// </summary>
        /// <param name="branchId">The ID of the branch to delete.</param>
        /// <returns>True if the branch was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int branchId);

        /// <summary>
        /// Retrieves all branches Option asynchronously.
        /// </summary>
        /// <returns>A collection of branch option.</returns>
        Task<IEnumerable<BranchOptionModel>?> GetAllBranchesOption();
    }
}
