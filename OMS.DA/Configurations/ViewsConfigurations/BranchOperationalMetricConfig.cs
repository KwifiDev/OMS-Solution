using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class BranchOperationalMetricConfig : IEntityTypeConfiguration<BranchOperationalMetric>
    {
        public void Configure(EntityTypeBuilder<BranchOperationalMetric> builder)
        {
            builder.ToView("BranchOperationalMetrics");
        }
    }
}
