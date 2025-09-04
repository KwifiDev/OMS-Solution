using System.ComponentModel.DataAnnotations;

namespace OMS.UI.APIs.Dtos.Tables;


public partial class AccountDto
{
    [Key]
    public int Id { get; set; }

    public required int ClientId { get; set; }

    public required string UserAccount { get; set; }

    public decimal Balance { get; set; } = 0;
}

