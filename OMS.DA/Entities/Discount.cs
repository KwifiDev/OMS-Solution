using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OMS.DA.Enums;

namespace OMS.DA.Entities;

[Index("ServiceId", "ClientType", Name = "unique_service_client", IsUnique = true)]
public partial class Discount
{
    [Key]
    public int DiscountId { get; set; }

    public int ServiceId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    public EnClientType ClientType { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal DiscountPercentage { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("Discounts")]
    public virtual Service Service { get; set; } = null!;
}
