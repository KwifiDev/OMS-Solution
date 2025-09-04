using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Tables
{
    public class AccountModel : BaseModel
    {
        private int _id;
        private int _clientId;
        private string _userAccount = null!;
        private decimal _balance;


        [Key]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int ClientId
        {
            get => _clientId;
            set => SetProperty(ref _clientId, value);
        }

        [Required(ErrorMessage = "حساب العميل مطلوب")]
        [MinLength(3, ErrorMessage = "حساب العميل على الاقل مكون من 3 احرف")]
        public string UserAccount
        {
            get => _userAccount;
            set => SetProperty(ref _userAccount, value);
        }

        public decimal Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }
    }
}
