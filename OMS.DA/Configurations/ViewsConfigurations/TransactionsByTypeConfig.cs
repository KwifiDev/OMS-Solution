using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class TransactionsByTypeConfig : IEntityTypeConfiguration<TransactionsByType>
    {
        public void Configure(EntityTypeBuilder<TransactionsByType> builder)
        {
            builder.ToView("TransactionsByType");
        }
    }
}
