using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class TransactionDto
{
    [Key]
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Account Id must be positive number")]
    public int AccountId { get; set; }

    [Range(1, 3, ErrorMessage = "TransactionType Must be Between [1:Deposit, 2:Withdraw, 3:Transfer]")]
    public EnTransactionType TransactionType { get; set; }

    [Range(500, 1000000, ErrorMessage = "Amount must be between (500 - 1,000,000)")]
    public decimal Amount { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "Notes Length Must be Between (5 - 100)")]
    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "CreatedByUserId must be positive number")]
    public int CreatedByUserId { get; set; }
}
