using OMS.BL.Models.Tables;
using OMS.Common.Enums;
using OMS.Common.Extensions.Pagination;
using System.Security.Claims;

namespace OMS.BL.IServices.Tables
{
    public interface IRoleClaimService
    {
        /// <summary>
        /// Retrieves all role claims asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of role claims models.</returns>
        Task<PagedResult<RoleClaimModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an role claims Model by roleId.
        /// </summary>
        /// <param name="roleId">The ID of the role.</param>
        /// <returns>The role claims Model if found; otherwise, null.</returns>
        Task<IEnumerable<RoleClaimModel>> GetRoleClaimsByRoleIdAsync(int roleId);

        /// <summary>
        /// Retrieves a role claim by its ID asynchronously.
        /// </summary>
        /// <param name="roleClaimId">The ID of the role claim to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role claim model, or null if not found.</returns>
        Task<RoleClaimModel?> GetByIdAsync(int roleClaimId);

        Task<EnRoleResult> AddRoleClaimAsync(int roleId, Claim claim);

        Task<EnRoleResult> RemoveRoleClaimAsync(int roleId, Claim claim);

        Task<IEnumerable<Claim>> GetRoleClaimsAsync(int roleId);
    }
}
