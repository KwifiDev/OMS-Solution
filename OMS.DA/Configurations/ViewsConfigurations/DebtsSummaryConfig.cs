using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class DebtsSummaryConfig : IEntityTypeConfiguration<DebtsSummary>
    {
        public void Configure(EntityTypeBuilder<DebtsSummary> builder)
        {
            builder.ToView("DebtsSummary");
        }
    }
}
