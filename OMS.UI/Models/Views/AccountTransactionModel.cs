using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class AccountTransactionModel : BaseModel
    {
        private int _accountId;
        private decimal _amount;
        private string? _notes;
        private int _createdByUserId;
        private EnTransactionType _transactionType;
        private EnAccountTransactionStatus _transactionStatus = EnAccountTransactionStatus.Empty;

        [Key]
        public int AccountId
        {
            get => _accountId;
            set => SetProperty(ref _accountId, value);
        }

        [Required(ErrorMessage = "حقل الرصيد مطلوب")]
        [Range(typeof(decimal), "1", "79228162514264337593543950335", ErrorMessage = "الرصيد يجب ان يكون موجب")]
        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value, validate: true);
        }
        public string? Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }
        public int CreatedByUserId
        {
            get => _createdByUserId;
            set => SetProperty(ref _createdByUserId, value);
        }
        public EnTransactionType TransactionType
        {
            get => _transactionType;
            set => SetProperty(ref _transactionType, value);
        }
        public EnAccountTransactionStatus TransactionStatus
        {
            get => _transactionStatus;
            set => SetProperty(ref _transactionStatus, value);
        }
    }
}
