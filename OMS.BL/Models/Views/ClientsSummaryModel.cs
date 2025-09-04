using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views
{
    public class ClientsSummaryModel : IModelKey
    {
        public int Id { get; set; }

        public int? AccountId { get; set; }

        public string FullName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string ClientType { get; set; } = null!;
    }
}
