using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class PaymentsSummaryConfig : IEntityTypeConfiguration<PaymentsSummary>
    {
        public void Configure(EntityTypeBuilder<PaymentsSummary> builder)
        {
            builder.ToView("PaymentsSummary");
        }
    }
}
