using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class TransactionsSummaryConfig : IEntityTypeConfiguration<TransactionsSummary>
    {
        public void Configure(EntityTypeBuilder<TransactionsSummary> builder)
        {
            builder.ToView("TransactionsSummary");
        }
    }
}
