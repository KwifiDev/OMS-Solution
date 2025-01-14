using OMS.BL.Dtos.Tables;

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
        Task<IEnumerable<BranchDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a branch by its ID asynchronously.
        /// </summary>
        /// <param name="branchId">The ID of the branch.</param>
        /// <returns>The branch dto if found, otherwise null.</returns>
        Task<BranchDto?> GetByIdAsync(int branchId);

        /// <summary>
        /// Adds a new branch asynchronously.
        /// </summary>
        /// <param name="dto">The branch dto to add.</param>
        /// <returns>True if the branch was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(BranchDto dto);

        /// <summary>
        /// Updates an existing branch asynchronously.
        /// </summary>
        /// <param name="dto">The branch dto to update.</param>
        /// <returns>True if the branch was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(BranchDto dto);

        /// <summary>
        /// Deletes a branch by its ID asynchronously.
        /// </summary>
        /// <param name="branchId">The ID of the branch to delete.</param>
        /// <returns>True if the branch was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int branchId);
    }
}
