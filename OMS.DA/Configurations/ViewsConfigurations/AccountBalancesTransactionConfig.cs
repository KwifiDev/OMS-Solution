using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;

namespace OMS.DA.Configurations.ViewsConfigurations
{
    public class AccountBalancesTransactionConfig : IEntityTypeConfiguration<AccountBalancesTransaction>
    {
        public void Configure(EntityTypeBuilder<AccountBalancesTransaction> builder)
        {
            builder.ToView("AccountBalancesTransactions");
        }
    }
}
