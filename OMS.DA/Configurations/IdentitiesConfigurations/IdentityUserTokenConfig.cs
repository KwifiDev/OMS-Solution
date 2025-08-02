using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities.Identity;

namespace OMS.DA.Configurations.IdentitiesConfigurations
{
    public class IdentityUserTokenConfig : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
        }
    }
}
