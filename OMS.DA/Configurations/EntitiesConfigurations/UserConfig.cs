using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities;

namespace OMS.DA.Configurations.EntitiesConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id).HasName("users_userid_primary");

            builder.Property(c => c.UserName).HasMaxLength(20);

            builder.HasOne(d => d.Branch).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_branchid_foreign");

            builder.HasOne(d => d.Person).WithOne(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_personid_foreign");

            builder.Ignore(u => u.Email);
            builder.Ignore(u => u.EmailConfirmed);
            builder.Ignore(u => u.NormalizedEmail);
            builder.Ignore(u => u.PhoneNumber);
            builder.Ignore(u => u.PhoneNumberConfirmed);
            builder.Ignore(u => u.TwoFactorEnabled);
            builder.Ignore(u => u.LockoutEnabled);
            builder.Ignore(u => u.LockoutEnd);
            builder.Ignore(u => u.AccessFailedCount);
        }
    }
}
