using OMS.Common.Enums;

namespace OMS.BL.Models.StoredProcedureParams
{
    public class PayDebtModel
    {
        public required int DebtId { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public EnPayDebtStatus PayDebtStatus { get; set; } = EnPayDebtStatus.Empty;

    }
}
