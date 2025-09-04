using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities;

namespace OMS.DA.Configurations.EntitiesConfigurations
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {

            builder.HasKey(e => e.Id).HasName("accounts_accountid_primary");

            builder.HasOne(d => d.Client).WithOne(p => p.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("accounts_clientid_foreign");

        }
    }
}
