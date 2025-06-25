using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views
{
    [Keyless]
    public class ServicesSummary
    {
        [Id]
        public int ServiceId { get; set; }

        [StringLength(25)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public int? TotalConsumed { get; set; }
    }
}
