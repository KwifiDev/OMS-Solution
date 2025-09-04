using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class PaymentDto
{
    [Key]
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }

    public int CreatedByUserId { get; set; }
}
