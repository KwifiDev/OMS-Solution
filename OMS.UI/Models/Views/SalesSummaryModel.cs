using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;

namespace OMS.UI.Models.Views
{
    public class SalesSummaryModel : ObservableObject
    {
        private int _id;
        private string _serviceName = null!;
        private string? _description;
        private string? _notes;
        private decimal? _totalSales;
        private EnSaleStatus _status;
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
                OnPropertyChanged(nameof(NotesDisplay));
            }
        }

        public decimal? TotalSales
        {
            get => _totalSales;
            set => SetProperty(ref _totalSales, value);
        }

        public EnSaleStatus Status
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
        public string StatusDisplay => Status == EnSaleStatus.Uncompleted ? "غير مكتمل" :
                                       Status == EnSaleStatus.Completed ? "مكتمل" : "ملغي";
    }
}
