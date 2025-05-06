using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;


public partial class AccountDto
{
    [Key]
    public int AccountId { get; internal set; }

    public required int ClientId { get; set; }

    public required string UserAccount { get; set; }

    public decimal Balance { get; set; } = 0;
}

