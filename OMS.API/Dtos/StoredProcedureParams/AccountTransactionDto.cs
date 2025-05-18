using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.StoredProcedureParams
{
    public class AccountTransactionDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Account Id Must Be Positive")]
        public required int AccountId { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(1000, 999000, ErrorMessage = "Amount Must Be Between [1,000 - 999,000]")]
        public required decimal Amount { get; set; }
        public string? Notes { get; set; }
        public required int CreatedByUserId { get; set; }

        [Range(1, 3, ErrorMessage = "TransactionType Must Be [1:Deposit, 2:Withdraw, 3:Transfer]")]
        public required EnTransactionType TransactionType { get; set; }
        public EnAccountTransactionStatus TransactionStatus { get; set; } = EnAccountTransactionStatus.Empty;
    }
}
