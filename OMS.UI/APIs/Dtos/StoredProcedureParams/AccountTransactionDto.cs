using OMS.Common.Enums;

namespace OMS.UI.APIs.Dtos.StoredProcedureParams
{
    public class AccountTransactionDto
    {
        public required int AccountId { get; set; }
        public required decimal Amount { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public EnTransactionType TransactionType { get; set; } = EnTransactionType.Empty;
        public EnAccountTransactionStatus TransactionStatus { get; set; } = EnAccountTransactionStatus.Empty;
    }
}
