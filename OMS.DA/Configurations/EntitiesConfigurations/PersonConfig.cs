using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Entities;
using OMS.DA.Enums;

namespace OMS.DA.Configurations.EntitiesConfigurations
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.PersonId).HasName("people_personid_primary");

            builder.Property(e => e.Gender).HasComment("0 = Male | 1 = Female");

            builder.Property(e => e.Gender)
                .HasConversion(
                  g => g == EnGender.Male,
                  g => g ? EnGender.Male : EnGender.Female
              );
        }
    }
}
