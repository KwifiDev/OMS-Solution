using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class SaleDto
{
    [Key]
    public int Id { get; set; }

    public int ClientId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Invalied Service Id")]
    public int ServiceId { get; set; }

    public decimal Cost { get; set; }

    [Range(1, short.MaxValue, ErrorMessage = "Invalied Quantity of Service")]
    public short Quantity { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal? AmountDeducted { get; set; }

    public decimal? Total { get; set; }

    [Length(3, 100, ErrorMessage = "Description Length Must be Between (3 - 100)")]
    public string? Description { get; set; }

    [Length(3, 100, ErrorMessage = "Notes Length Must be Between (3 - 100)")]
    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }

    /// <summary>
    /// 0 = Uncompleted | 1 = Completed | 2 = Canceled
    /// </summary>
    public EnSaleStatus Status { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Invalied CreatedByUserId")]
    public int CreatedByUserId { get; set; }
}
