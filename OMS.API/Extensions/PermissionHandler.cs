using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using OMS.DA.Entities.Identity;
using System.Security.Claims;

namespace OMS.API.Extensions
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMemoryCache _cache;

        public PermissionHandler(RoleManager<Role> roleManager, IMemoryCache cache)
        {
            _roleManager = roleManager;
            _cache = cache;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity is null) return;

            if (!context.User.Identity.IsAuthenticated) return;

            var roles = context.User.FindAll(ClaimTypes.Role).Select(c => c.Value);
            if (!roles.Any()) return;

            var cacheKey = $"Permissions_{string.Join("_", roles.OrderBy(r => r))}";

            if (!_cache.TryGetValue(cacheKey, out HashSet<string>? permissions))
            {
                permissions = new HashSet<string>();

                foreach (var roleName in roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role == null) continue;

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    var permClaims = roleClaims.Where(c => c.Type == "Permission").Select(c => c.Value);

                    foreach (var perm in permClaims)
                    {
                        permissions.Add(perm);
                    }
                }

                _cache.Set(cacheKey, permissions, TimeSpan.FromMinutes(1));
            }

            if (permissions is not null && permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}