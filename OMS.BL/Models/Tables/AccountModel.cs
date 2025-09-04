using OMS.BL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;


public partial class AccountModel : IModelKey
{
    [Key]
    public int Id { get; set; }

    public required int ClientId { get; set; }

    public required string UserAccount { get; set; }

    public decimal Balance { get; set; } = 0;
}

