using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class DiscountsAppliedConfig : IEntityTypeConfiguration<DiscountsApplied>
    {
        public void Configure(EntityTypeBuilder<DiscountsApplied> builder)
        {
            builder.ToView("DiscountsApplied");
        }
    }
}
