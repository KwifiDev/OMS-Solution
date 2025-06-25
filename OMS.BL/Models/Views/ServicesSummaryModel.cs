namespace OMS.BL.Models.Views
{
    public class ServicesSummaryModel
    {
        public int ServiceId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int? TotalConsumed { get; set; }
    }
}
