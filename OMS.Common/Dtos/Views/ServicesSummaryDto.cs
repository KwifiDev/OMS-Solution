namespace OMS.Common.Dtos.Views
{
    public class ServicesSummaryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int? TotalConsumed { get; set; }
    }
}
