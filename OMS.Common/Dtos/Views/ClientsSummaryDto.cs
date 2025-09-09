namespace OMS.Common.Dtos.Views
{
    public class ClientsSummaryDto
    {
        public int Id { get; set; }

        public int? AccountId { get; set; }

        public string FullName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string ClientType { get; set; } = null!;
    }
}
