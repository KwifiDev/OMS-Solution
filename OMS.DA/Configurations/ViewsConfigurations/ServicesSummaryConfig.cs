using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class ServicesSummaryConfig : IEntityTypeConfiguration<ServicesSummary>
    {
        public void Configure(EntityTypeBuilder<ServicesSummary> builder)
        {
            builder.ToView("ServicesSummary");
        }
    }
}
