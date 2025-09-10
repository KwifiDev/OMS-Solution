using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Others
{
    public class AccountTransactionModel : BaseModel
    {
        private int _accountId;
        private decimal _amount = 1000;
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
        [Range(typeof(decimal), "1000", "1000000", ErrorMessage = "الرصيد يجب ان يكون موجب")]
        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value, validate: true);
        }

        [MinLength(5, ErrorMessage = "الملاحظات على الاقل مكونة من 5 احرف")]
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

        [Range(1, 3, ErrorMessage = "حدد نوع المناقلة بشكل صحيح")]
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
