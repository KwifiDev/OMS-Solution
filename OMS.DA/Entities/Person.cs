using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.Entities.Identity;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

public partial class Person : IEntityKey
{
    [Key]
    [Column("PersonId")]
    public int Id { get; set; }

    [StringLength(20)]
    public string FirstName { get; set; } = null!;

    [StringLength(20)]
    public string LastName { get; set; } = null!;

    /// <summary>
    /// 0 = Male | 1 = Female
    /// </summary>
    public EnGender Gender { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [InverseProperty("Person")]
    public virtual Client? Client { get; set; }

    [InverseProperty("Person")]
    public virtual User? User { get; set; }
}
