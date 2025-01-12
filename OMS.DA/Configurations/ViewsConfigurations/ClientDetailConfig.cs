using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class ClientDetailConfig : IEntityTypeConfiguration<ClientDetail>
    {
        public void Configure(EntityTypeBuilder<ClientDetail> builder)
        {
            builder.ToView("ClientDetails");
        }
    }
}
