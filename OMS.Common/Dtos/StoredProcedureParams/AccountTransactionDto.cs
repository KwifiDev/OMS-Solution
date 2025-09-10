using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.StoredProcedureParams
{
    public class AccountTransactionDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Account Id Must Be Positive")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(1000, 999000, ErrorMessage = "Amount Must Be Between [1,000 - 999,000]")]
        public decimal Amount { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Notes Length must be between (5 - 100)")]
        public string? Notes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CreatedByUserId Must Be Positive")]
        public int CreatedByUserId { get; set; }

        [Range(1, 3, ErrorMessage = "TransactionType Must Be [1:Deposit, 2:Withdraw, 3:Transfer]")]
        public EnTransactionType TransactionType { get; set; }
        public EnAccountTransactionStatus TransactionStatus { get; set; } = EnAccountTransactionStatus.Empty;
    }
}
