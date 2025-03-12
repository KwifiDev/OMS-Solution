using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.Common.Enums;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class PersonDetailConfig : IEntityTypeConfiguration<PersonDetail>
    {
        public void Configure(EntityTypeBuilder<PersonDetail> builder)
        {
            builder.ToView("PersonDetails");

            builder.Property(e => e.Gender)
                .HasConversion(
                    g => g == EnGender.Male,
                    g => g ? EnGender.Male : EnGender.Female
                );
        }
    }
}
