using Microsoft.AspNetCore.Identity;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Tables;
using OMS.BL.Services.Views;
using OMS.Common.Enums;
using OMS.DA.Entities.Identity;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using System.Security.Claims;

namespace OMS.BL.Services.Tables
{
    public class RoleClaimService : GenericViewService<RoleClaim, RoleClaimModel>, IRoleClaimService
    {
        private readonly IRoleClaimRepository _roleClaimRepository;
        private readonly RoleManager<Role> _roleManager;

        public RoleClaimService(IGenericViewRepository<RoleClaim> repository, IMapperService mapper,
                                IRoleClaimRepository roleClaimRepository, RoleManager<Role> roleManager) : base(repository, mapper)
        {
            _roleClaimRepository = roleClaimRepository;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<RoleClaimModel>> GetRoleClaimsByRoleIdAsync(int roleId)
        {
            var roleClaims = await _roleClaimRepository.GetRoleClaimsByRoleId(roleId);
            if (roleClaims is null) return Enumerable.Empty<RoleClaimModel>();

            return _mapper.Map<IEnumerable<RoleClaim>, IEnumerable<RoleClaimModel>>(roleClaims);
        }

        public async Task<EnRoleResult> AddRoleClaimAsync(int roleId, Claim claim)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null) return EnRoleResult.NotFound;

            var isExist = (await _roleManager.GetClaimsAsync(role)).Any(c => c.Type == claim.Type && c.Value == claim.Value);
            if (isExist) return EnRoleResult.RoleConflict;

            var result = await _roleManager.AddClaimAsync(role, claim);

            return result.Succeeded ? EnRoleResult.Success : EnRoleResult.Failed;
        }

        public async Task<EnRoleResult> RemoveRoleClaimAsync(int roleId, Claim claim)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null) return EnRoleResult.NotFound;

            var isExist = (await _roleManager.GetClaimsAsync(role)).Any(c => c.Type == claim.Type && c.Value == claim.Value);
            if (!isExist) return EnRoleResult.NotFound;

            var result = await _roleManager.RemoveClaimAsync(role, claim);

            return result.Succeeded ? EnRoleResult.Success : EnRoleResult.Failed;
        }

        public async Task<IEnumerable<Claim>> GetRoleClaimsAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null) return Enumerable.Empty<Claim>();

            return await _roleManager.GetClaimsAsync(role);
        }
    }
}
