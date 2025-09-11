using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class PaymentsSummaryModel : BaseModel
    {
        private int _id;
        private string? _amountPaid;
        private DateOnly _createdAt;
        private string? _notes;
        private string _employeeName = null!;

        [Key]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string? AmountPaid
        {
            get => _amountPaid;
            set => SetProperty(ref _amountPaid, value);
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

        public string EmployeeName
        {
            get => _employeeName;
            set => SetProperty(ref _employeeName, value);
        }

        // Display Props
        public string NotesDisplay => Notes ?? "لا توجد ملاحظات";

    }
}
