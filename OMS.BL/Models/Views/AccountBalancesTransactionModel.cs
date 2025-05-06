namespace OMS.BL.Models.Views;

public partial class AccountBalancesTransactionModel
{
    public int AccountId { get; set; }

    public string ClientName { get; set; } = null!;

    public string UserAccount { get; set; } = null!;

    public string? AccountBalance { get; set; }

    public int? TotalTransactions { get; set; }

    public decimal? TotalTransactionAmount { get; set; }
}
