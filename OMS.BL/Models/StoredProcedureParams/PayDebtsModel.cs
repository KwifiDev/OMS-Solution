using OMS.Common.Enums;

namespace OMS.BL.Models.StoredProcedureParams
{
    public class PayDebtsModel
    {
        public required int ClientId { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public EnPayDebtStatus PayDebtStatus { get; set; } = EnPayDebtStatus.Empty;

    }
}
