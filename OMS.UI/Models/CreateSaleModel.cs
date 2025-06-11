using OMS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.UI.Models
{
    public class CreateSaleModel
    {
        public int SaleId { get; set; }

        public int ClientId { get; set; }

        public int ServiceId { get; set; }

        public short Quantity { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public EnSaleStatus Status { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
