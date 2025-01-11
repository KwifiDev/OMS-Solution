using OMS.DA.Enums;

namespace OMS.BL.Models.DTOs_StoredProcedures
{
    public class PayDebtsModel
    {
        public required int ClientId { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public EnPayDebtStatus PayDebtStatus { get; set; } = EnPayDebtStatus.Empty;

    }
}
