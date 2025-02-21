using OMS.Common.Enums;

namespace OMS.BL.Dtos.StoredProcedureParams
{
    public class PayDebtsDto
    {
        public required int ClientId { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public EnPayDebtStatus PayDebtStatus { get; set; } = EnPayDebtStatus.Empty;

    }
}
