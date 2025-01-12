using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class DebtsByStatusConfig : IEntityTypeConfiguration<DebtsByStatus>
    {
        public void Configure(EntityTypeBuilder<DebtsByStatus> builder)
        {
            builder.ToView("DebtsByStatus");
        }
    }
}
