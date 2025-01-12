using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

[Index("ClientId", Name = "accounts_clientid_unique", IsUnique = true)]
[Index("UserAccount", Name = "accounts_useraccount_unique", IsUnique = true)]
public partial class Account
{
    [Key]
    public int AccountId { get; set; }

    public int ClientId { get; set; }

    [StringLength(20)]
    public string UserAccount { get; set; } = null!;

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Balance { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Account")]
    public virtual Client Client { get; set; } = null!;

    [InverseProperty("Account")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("Account")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

