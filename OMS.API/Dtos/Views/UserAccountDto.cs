namespace OMS.API.Dtos.Views;

public partial class UserAccountDto
{
    public int AccountId { get; set; }

    public string UserAccount1 { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string ClientType { get; set; } = null!;

    public decimal ClientBalance { get; set; }
}
