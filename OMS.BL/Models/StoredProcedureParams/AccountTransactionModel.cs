using OMS.Common.Enums;

namespace OMS.BL.Models.StoredProcedureParams
{
    public class AccountTransactionModel
    {
        public required int AccountId { get; set; }
        public required decimal Amount { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public required EnTransactionType TransactionType { get; set; }
        public EnAccountTransactionStatus TransactionStatus { get; internal set; } = EnAccountTransactionStatus.Empty;
    }
}
