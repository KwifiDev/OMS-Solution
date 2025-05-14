namespace OMS.UI.APIs.Dtos.Views;

public partial class AccountBalancesTransactionDto
{
    public int AccountId { get; set; }

    public string ClientName { get; set; } = null!;

    public string UserAccount { get; set; } = null!;

    public string? AccountBalance { get; set; }

    public int? TotalTransactions { get; set; }

    public decimal? TotalTransactionAmount { get; set; }
}
