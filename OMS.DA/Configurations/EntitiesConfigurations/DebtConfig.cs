using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities;

namespace OMS.DA.Configurations.EntitiesConfigurations
{
    public class DebtConfig : IEntityTypeConfiguration<Debt>
    {
        public void Configure(EntityTypeBuilder<Debt> builder)
        {
            builder.HasKey(e => e.Id).HasName("debts_debtid_primary");

            builder.ToTable(tb => tb.HasTrigger("TR_InsertNewDebt"));

            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Status).HasComment("0 = NotPaid | 1 = Paid | 2 = Canceled");
            builder.Property(e => e.Total).HasComputedColumnSql("([Cost]*[Quantity])", true);

            builder.HasOne(d => d.Client).WithMany(p => p.Debts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("debts_clientid_foreign");

            builder.HasOne(d => d.CreatedByUser).WithMany(p => p.Debts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("debts_createdbyuserid_foreign");

            builder.HasOne(d => d.Payment).WithMany(p => p.Debts).HasConstraintName("debts_paymentid_foreign");

            builder.HasOne(d => d.Service).WithMany(p => p.Debts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("debts_serviceid_foreign");
        }
    }
}
