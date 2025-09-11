using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;

namespace OMS.UI.Models.Views
{
    public class DebtsSummaryModel : ObservableObject
    {
        private int _id;
        private string _serviceName = null!;
        private string? _description;
        private string? _notes;
        private decimal? _totalDebts;
        private EnDebtStatus _status;
        private DateOnly _createdAt;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string ServiceName
        {
            get => _serviceName;
            set => SetProperty(ref _serviceName, value);
        }

        public string? Description
        {
            get => _description;
            set 
            {
                SetProperty(ref _description, value);
                OnPropertyChanged(nameof(DescriptionDisplay));
            }
        }

        public string? Notes
        {
            get => _notes;
            set 
            {
                SetProperty(ref _notes, value);
                OnPropertyChanged(NotesDisplay);
            }
        }

        public decimal? TotalDebts
        {
            get => _totalDebts;
            set => SetProperty(ref _totalDebts, value);
        }

        public EnDebtStatus Status
        {
            get => _status;
            set 
            {
                SetProperty(ref _status, value);
                OnPropertyChanged(nameof(StatusDisplay));
            }
        }

        public DateOnly CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        // Display Props
        public string DescriptionDisplay => Description ?? "لا يوجد وصف";
        public string NotesDisplay => Notes ?? "لا توجد ملاحظات";
        public string StatusDisplay => Status == EnDebtStatus.NotPaid ? "غير مدفوع" :
                                       Status == EnDebtStatus.Paid ? "مدفوع" : "ملغي";
    }
}
