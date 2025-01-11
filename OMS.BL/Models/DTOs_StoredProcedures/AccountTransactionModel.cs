using OMS.DA.Enums;

namespace OMS.BL.Models.DTOs_StoredProcedures
{
    public class AccountTransactionModel
    {
        public required int AccountId { get; set; }
        public required decimal Amount { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }
        public EnAccountTransactionStatus TransactionStatus { get; internal set; } = EnAccountTransactionStatus.Empty;
    }
}
