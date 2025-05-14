using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public class ClientsSummaryModel
    {
        [Key]
        public int ClientId { get; set; }

        public int? AccountId { get; set; }

        public string FullName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string ClientType { get; set; } = null!;
    }
}
