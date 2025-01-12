using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class MonthlyFinancialSummaryConfig : IEntityTypeConfiguration<MonthlyFinancialSummary>
    {
        public void Configure(EntityTypeBuilder<MonthlyFinancialSummary> builder)
        {
            builder.ToView("MonthlyFinancialSummary");
        }
    }
}
