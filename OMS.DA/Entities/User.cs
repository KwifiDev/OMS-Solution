using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OMS.DA.Entities;

[Index("PersonId", Name = "users_personid_unique", IsUnique = true)]
[Index("Username", Name = "users_username_unique", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    public int PersonId { get; set; }

    public int BranchId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(64)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    public int Permissions { get; set; }

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
