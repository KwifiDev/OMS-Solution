using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class TransactionsSummaryModel : ObservableObject
    {

        private int _id;
        private string _transactionType = null!;
        private string? _amount = null!;
        private DateOnly _createdAt;
        private string _notes = null!;


        [Key]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string TransactionType
        {
            get => _transactionType;
            set => SetProperty(ref _transactionType, value);
        }

        public string? Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public DateOnly CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

    }
}
