using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views
{
    [Keyless]
    public class ServicesSummary
    {
        [Id]
        public int ServiceId { get; set; }

        [StringLength(25)]
        public string Name { get; set; } = null!;

        public decimal? Price { get; set; }

        public int? TotalConsumed { get; set; }
    }
}
