using Microsoft.EntityFrameworkCore;
using OMS.DA.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

[Index("PersonId", Name = "clients_personid_unique", IsUnique = true)]
public partial class Client
{
    [Key]
    public int ClientId { get; set; }

    public int PersonId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    public EnClientType ClientType { get; set; }

    [InverseProperty("Client")]
    public virtual Account? Account { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();

    [ForeignKey("PersonId")]
    [InverseProperty("Client")]
    public virtual Person Person { get; set; } = null!;

    [InverseProperty("Client")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
