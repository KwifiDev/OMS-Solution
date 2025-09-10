using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class SaleDto
{
    [Key]
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Client Id must positive number")]
    public int ClientId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Service Id must positive number")]
    public int ServiceId { get; set; }

    public decimal Cost { get; set; }

    [Range(1, 100, ErrorMessage = "Quantity must be between (1 - 100)")]
    public short Quantity { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal? AmountDeducted { get; set; }

    public decimal? Total { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "Description Length Must be Between (5 - 100)")]
    public string? Description { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "Notes Length Must be Between (5 - 100)")]
    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }

    [Range(0, 2, ErrorMessage = "Sale Status must be between [0:Uncompleted, 1:Completed, 2:Canceled]")]
    public EnSaleStatus Status { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "CreatedByUserId must positive number")]
    public int CreatedByUserId { get; set; }
}
