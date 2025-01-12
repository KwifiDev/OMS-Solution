using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities;

namespace OMS.DA.Configurations.EntitiesConfigurations
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(e => e.SaleId).HasName("sales_saleid_primary");

            builder.ToTable(tb => tb.HasTrigger("TR_InsertNewSale"));

            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Status).HasComment("0 = Uncompleted | 1 = Completed | 2 = Canceled");
            builder.Property(e => e.Total).HasComputedColumnSql("([Cost]*[Quantity])", true);

            builder.HasOne(d => d.Client).WithMany(p => p.Sales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_clientid_foreign");

            builder.HasOne(d => d.Service).WithMany(p => p.Sales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_serviceid_foreign");
        }
    }
}
