using OMS.DA.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

public partial class Branch
{
    [Key]
    public int BranchId { get; set; }

    [StringLength(20)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Address { get; set; } = null!;

    [InverseProperty("Branch")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
