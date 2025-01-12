using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class ClientsByTypeConfig : IEntityTypeConfiguration<ClientsByType>
    {
        public void Configure(EntityTypeBuilder<ClientsByType> builder)
        {
            builder.ToView("ClientsByType");
        }
    }
}
