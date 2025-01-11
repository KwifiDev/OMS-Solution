using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.DA.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
