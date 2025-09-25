using OMS.BL.Interfaces;
using OMS.Common.Enums;

namespace OMS.BL.Models.Views
{
    public class ClientsSummaryModel : IModelKey
    {
        public int Id { get; set; }

        public int? AccountId { get; set; }

        public string FullName { get; set; } = null!;

        public string? Phone { get; set; }

        public EnClientType ClientType { get; set; }

        public decimal TotalDebts { get; set; }
    }
}
