namespace OMS.UI.APIs.Dtos.Views
{
    public class ServicesSummaryDto
    {
        public int ServiceId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int? TotalConsumed { get; set; }
    }
}
