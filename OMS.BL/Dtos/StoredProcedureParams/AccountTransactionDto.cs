using OMS.Common.Enums;

namespace OMS.BL.Dtos.StoredProcedureParams
{
    public class AccountTransactionDto
    {
        public required int AccountId { get; set; }
        public required decimal Amount { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public EnAccountTransactionStatus TransactionStatus { get; internal set; } = EnAccountTransactionStatus.Empty;
    }
}
