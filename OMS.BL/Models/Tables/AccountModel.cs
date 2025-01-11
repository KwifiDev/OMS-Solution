namespace OMS.BL.Models.Tables;


public partial class AccountModel
{
    public int AccountId { get; internal set; }

    public required int ClientId { get; set; }

    public required string UserAccount { get; set; }

    public decimal Balance { get; set; } = 0;
}

