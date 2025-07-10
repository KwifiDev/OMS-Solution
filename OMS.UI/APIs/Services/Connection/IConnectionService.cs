namespace OMS.UI.APIs.Services.Connection
{
    public interface IConnectionService
    {
        Task<bool> VerifyServerConnectionAsync();
    }
}
