using OMS.BL.Models.Tables;

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
        Task<IEnumerable<BranchModel>> GetAllBranchesAsync();

        /// <summary>
        /// Retrieves a branch by its ID asynchronously.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The branch model if found, otherwise null.</returns>
        Task<BranchModel?> GetBranchByIdAsync(int branchId);

        /// <summary>
        /// Adds a new branch asynchronously.
        /// </summary>
        /// <param name="model">The branch model to add.</param>
        /// <returns>True if the branch was added successfully, otherwise false.</returns>
        Task<bool> AddBranchAsync(BranchModel model);

        /// <summary>
        /// Updates an existing branch asynchronously.
        /// </summary>
        /// <param name="model">The branch model to update.</param>
        /// <returns>True if the branch was updated successfully, otherwise false.</returns>
        Task<bool> UpdateBranchAsync(BranchModel model);

        /// <summary>
        /// Deletes a branch by its ID asynchronously.
        /// </summary>
        /// <param name="branchId">The ID of the branch to delete.</param>
        /// <returns>True if the branch was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteBranchAsync(int branchId);
    }
}
