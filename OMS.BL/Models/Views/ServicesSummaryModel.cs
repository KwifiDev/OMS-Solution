using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views
{
    public class ServicesSummaryModel : IModelKey
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int? TotalConsumed { get; set; }
    }
}
