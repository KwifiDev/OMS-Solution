using OMS.DA.Enums;

namespace OMS.BL.Dtos.StoredProcedureParams
{
    public class PayDebtDto
    {
        public required int DebtId { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public EnPayDebtStatus PayDebtStatus { get; set; } = EnPayDebtStatus.Empty;

    }
}
