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
    public class SalesSummaryConfig : IEntityTypeConfiguration<SalesSummary>
    {
        public void Configure(EntityTypeBuilder<SalesSummary> builder)
        {
            builder.ToView("SalesSummary");
        }
    }
}
