using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities;

namespace OMS.DA.Configurations.EntitiesConfigurations
{
    public class RevenueConfig : IEntityTypeConfiguration<Revenue>
    {
        public void Configure(EntityTypeBuilder<Revenue> builder)
        {
            builder.HasKey(e => e.RevenueId).HasName("revenues_revenueid_primary");

            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        }
    }
}
