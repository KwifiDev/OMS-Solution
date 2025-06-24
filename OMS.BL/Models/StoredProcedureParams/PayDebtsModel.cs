using OMS.Common.Enums;

namespace OMS.BL.Models.StoredProcedureParams
{
    public class PayDebtsModel
    {
        public int ClientId { get; set; }
        public string? Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public EnPayDebtStatus PayDebtStatus { get; set; } = EnPayDebtStatus.Empty;

    }
}
