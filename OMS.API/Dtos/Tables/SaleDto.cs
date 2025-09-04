using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class SaleDto
{
    [Key]
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int ServiceId { get; set; }

    public decimal Cost { get; set; }

    public short Quantity { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal? AmountDeducted { get; set; }

    public decimal? Total { get; set; }

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }

    /// <summary>
    /// 0 = Uncompleted | 1 = Completed | 2 = Canceled
    /// </summary>
    public EnSaleStatus Status { get; set; }

    public int CreatedByUserId { get; set; }
}
