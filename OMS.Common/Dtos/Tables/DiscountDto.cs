using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class DiscountDto
{
    [Key]
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Service Id must be positive number")]
    public int ServiceId { get; set; }

    [Range(0, 2, ErrorMessage = "ClientType must Be between [0:Normal, 1:Lawyer, 2:Other]")]
    public EnClientType ClientType { get; set; }

    [Range(1, 99, ErrorMessage = "DiscountPercentage must be between [1 - 99]")]
    public decimal DiscountPercentage { get; set; }
}
