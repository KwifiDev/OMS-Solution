using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class DashboardSummaryConfig : IEntityTypeConfiguration<DashboardSummary>
    {
        public void Configure(EntityTypeBuilder<DashboardSummary> builder)
        {
            builder.ToView("DashboardSummary");
        }
    }
}
