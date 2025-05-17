using OMS.Common.Enums;

namespace OMS.API.Dtos.StoredProcedureParams
{
    public class AccountTransactionDto
    {
        public required int AccountId { get; set; }
        public required decimal Amount { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public required EnTransactionType TransactionType { get; set; }
        public EnAccountTransactionStatus TransactionStatus { get; set; } = EnAccountTransactionStatus.Empty;
    }
}
