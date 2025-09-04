using OMS.BL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;

public partial class RevenueModel : IModelKey
{
    [Key]
    public int Id { get; set; }

    public required decimal Amount { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }
}
