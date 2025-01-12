using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities;

namespace OMS.DA.Configurations.EntitiesConfigurations
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(e => e.ClientId).HasName("clients_clientid_primary");

            builder.Property(e => e.ClientType).HasComment("0 = Normal | 1 = Lawyer | 2 = Other");

            builder.HasOne(d => d.Person).WithOne(p => p.Client)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clients_personid_foreign");
        }
    }
}
