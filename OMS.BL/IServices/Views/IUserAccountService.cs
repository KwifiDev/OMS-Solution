using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IUserAccountService
    {
        Task<IEnumerable<UserAccountModel>> GetAllUsersAccountsAsync();
        Task<UserAccountModel?> GetUserAccountByIdAsync(int accountId);
    }
}
