using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class ClientsSummaryConfig : IEntityTypeConfiguration<ClientsSummary>
    {
        public void Configure(EntityTypeBuilder<ClientsSummary> builder)
        {
            builder.ToView("ClientsSummary");
        }
    }
}
