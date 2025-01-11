using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DA.Configurations.EntitiesConfigurations
{
    public class DiscountConfig : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(e => e.DiscountId).HasName("discounts_discountid_primary");

            builder.Property(e => e.ClientType).HasComment("0 = Normal | 1 = Lawyer | 2 = Other");

            builder.HasOne(d => d.Service).WithMany(p => p.Discounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("discounts_serviceid_foreign");
        }
    }
}
