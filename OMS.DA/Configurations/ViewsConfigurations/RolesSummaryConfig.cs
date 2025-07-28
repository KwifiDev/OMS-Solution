using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class RolesSummaryConfig : IEntityTypeConfiguration<RolesSummary>
    {
        public void Configure(EntityTypeBuilder<RolesSummary> builder)
        {
            builder.ToView("RolesSummary");
        }
    }
}
