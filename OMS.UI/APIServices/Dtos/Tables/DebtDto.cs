using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.APIServices.Dtos.Tables;

public partial class DebtDto
{
    [Key]
    public int DebtId { get; internal set; }

    public required int ClientId { get; set; }

    public required int ServiceId { get; set; }

    public decimal Cost { get; set; }

    public required short Quantity { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal? AmountDeducted { get; set; }

    public decimal? Total { get; set; }

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; internal set; }

    /// <summary>
    /// 0 = NotPaid | 1 = Paid | 2 = Canceled
    /// </summary>
    public EnDebtStatus Status { get; set; } = EnDebtStatus.NotPaid;

    public int? PaymentId { get; set; }

    public required int CreatedByUserId { get; set; }
}
