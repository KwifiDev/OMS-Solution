using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace OMS.API.Extensions
{
    public class PermissionCacheService : IPermissionCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentBag<string> _permissionKeys = new();

        public PermissionCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void TrackPermissionKey(string key)
        {
            if (key.StartsWith("Permissions_"))
            {
                _permissionKeys.Add(key);
            }
        }

        public void ClearPermissionCache()
        {
            if (_permissionKeys.IsEmpty)
            {
                (_memoryCache as MemoryCache)?.Clear();
                return;
            }

            foreach (var key in _permissionKeys.ToArray())
            {
                _memoryCache.Remove(key);
                _permissionKeys.TryTake(out _);
            }
        }

        public IEnumerable<string> GetPermissionKeys()
        {
            return _permissionKeys.ToArray();
        }
    }
}
