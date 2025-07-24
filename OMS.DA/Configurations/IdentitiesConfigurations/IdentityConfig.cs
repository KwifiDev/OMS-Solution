using Microsoft.EntityFrameworkCore;

namespace OMS.DA.Configurations.IdentitiesConfigurations
{
    public static class IdentityConfig
    {
        public static void ConfigureAll(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new IdentityUserLoginConfig());
            builder.ApplyConfiguration(new IdentityUserClaimConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new IdentityUserTokenConfig());
            builder.ApplyConfiguration(new IdentityRoleConfig());
            builder.ApplyConfiguration(new IdentityRoleClaimConfig());
        }
    }
}
