using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class BranchOptionConfig : IEntityTypeConfiguration<BranchOption>
    {
        public void Configure(EntityTypeBuilder<BranchOption> builder)
        {
            builder.ToView("BranchOptions");
        }
    }
}
