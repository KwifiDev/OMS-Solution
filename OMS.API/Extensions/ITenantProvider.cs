using OMS.BL.Models.Hybrid;

namespace OMS.API.Extensions
{
    public interface ITenantProvider
    {
        TenantModel CurrentTenant { get; }
        void SetTenant(TenantModel tenant);

        TenantModel? GetFromJwtClaim();
        TenantModel? GetFromHeader();
    }
}
