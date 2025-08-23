using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace OMS.API.Extensions
{
    public class ClearPermissionCacheFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache _cache;
        private readonly IPermissionCacheService _cacheService;

        public ClearPermissionCacheFilter(IMemoryCache cache, IPermissionCacheService cacheService)
        {
            _cache = cache;
            _cacheService = cacheService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _cacheService.ClearPermissionCache();

            await next();
        }
    }
}
