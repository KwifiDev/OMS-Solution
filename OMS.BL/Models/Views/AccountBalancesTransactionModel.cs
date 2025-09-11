using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views;

public partial class AccountBalancesTransactionModel : IModelKey
{
    public int Id { get; set; }

    public string ClientName { get; set; } = null!;

    public string UserAccount { get; set; } = null!;

    public decimal AccountBalance { get; set; }

    public int? TotalTransactions { get; set; }

    public decimal? TotalTransactionAmount { get; set; }
}
