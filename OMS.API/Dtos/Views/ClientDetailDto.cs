namespace OMS.API.Dtos.Views;

public partial class ClientDetailDto
{
    public int Id { get; set; }

    public string ClientName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string ClientType { get; set; } = null!;
}
