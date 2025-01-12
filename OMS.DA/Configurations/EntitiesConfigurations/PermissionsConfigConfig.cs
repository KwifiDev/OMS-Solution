using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities;

namespace OMS.DA.Configurations.EntitiesConfigurations
{
    public class PermissionsConfigConfig : IEntityTypeConfiguration<PermissionsConfig>
    {
        public void Configure(EntityTypeBuilder<PermissionsConfig> builder)
        {
            builder.HasKey(e => e.PermissionConfigId).HasName("permissionsconfig_permissionconfigid_primary");

            builder.Property(e => e.PermissionNo).HasComment("Bit-Wise Operator Example 1, 2, 4, 8, 16, 32, ...");
        }
    }
}
