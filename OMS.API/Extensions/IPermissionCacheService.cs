namespace OMS.API.Extensions
{
    public interface IPermissionCacheService
    {
        void TrackPermissionKey(string key);
        void ClearPermissionCache();
        IEnumerable<string> GetPermissionKeys();
    }
}
