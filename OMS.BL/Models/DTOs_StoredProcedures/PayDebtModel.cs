using OMS.DA.Enums;

namespace OMS.BL.Models.DTOs_StoredProcedures
{
    public class PayDebtModel
    {
        public required int DebtId { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public EnPayDebtStatus PayDebtStatus { get; set; } = EnPayDebtStatus.Empty;

    }
}
