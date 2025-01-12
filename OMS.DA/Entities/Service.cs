using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

public partial class Service
{
    [Key]
    public int ServiceId { get; set; }

    [StringLength(25)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();

    [InverseProperty("Service")]
    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    [InverseProperty("Service")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
