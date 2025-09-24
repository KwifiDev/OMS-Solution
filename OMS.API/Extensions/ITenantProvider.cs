using OMS.API.Models;

namespace OMS.API.Extensions
{
    public interface ITenantProvider
    {
        TenantModel CurrentTenant { get; }
        void SetTenant(TenantModel tenant);

        TenantModel? GetFromJwtClaim();
        TenantModel? GetFromHeader();
        TenantModel? GetLocal();
    }
}
