using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities.Identity;

[Index("PersonId", Name = "users_personid_unique", IsUnique = true)]
[Index("UserName", Name = "users_username_unique", IsUnique = true)]
public partial class User : IdentityUser<int>, IEntityKey
{

    [Key]
    [Column("UserId")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public int PersonId { get; set; }

    public int BranchId { get; set; }

    [Required, Column("Username")]
    [Unicode(false)]
    public override string? UserName { get => base.UserName; set => base.UserName = value; }

    [Required, Column("Password")]
    [StringLength(128)]
    [Unicode(false)]
    public override string? PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

    public bool IsActive { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Users")]
    public virtual Branch Branch { get; set; } = null!;

    [InverseProperty("CreatedByUser")]
    public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();

    [InverseProperty("CreatedByUser")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("PersonId")]
    [InverseProperty("User")]
    public virtual Person Person { get; set; } = null!;

    [InverseProperty("CreatedByUser")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
