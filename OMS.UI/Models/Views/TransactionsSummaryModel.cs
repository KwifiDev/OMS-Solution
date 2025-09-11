using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class TransactionsSummaryModel : ObservableObject
    {

        private int _id;
        private EnTransactionType _transactionType;
        private decimal _amount;
        private DateOnly _createdAt;
        private string? _notes;


        [Key]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public EnTransactionType TransactionType
        {
            get => _transactionType;
            set 
            {
                SetProperty(ref _transactionType, value);
                OnPropertyChanged(nameof(TransactionTypeDisplay));
            }
        }

        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public DateOnly CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        public string? Notes
        {
            get => _notes;
            set
            {
                SetProperty(ref _notes, value);
                OnPropertyChanged(nameof(NotesDisplay));
            }
        }

        // Display Props
        public string NotesDisplay => Notes ?? "لا توجد ملاحظات";
        public string TransactionTypeDisplay => TransactionType == EnTransactionType.Deposit ? "إيداع" :
                                                TransactionType == EnTransactionType.Withdraw ? "سحب" :
                                                TransactionType == EnTransactionType.Transfer ? "تحويل" : "أخرى";

    }
}
