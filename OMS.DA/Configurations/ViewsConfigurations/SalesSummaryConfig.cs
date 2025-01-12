using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class SalesSummaryConfig : IEntityTypeConfiguration<SalesSummary>
    {
        public void Configure(EntityTypeBuilder<SalesSummary> builder)
        {
            builder.ToView("SalesSummary");
        }
    }
}
