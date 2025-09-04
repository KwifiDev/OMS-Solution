using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Models.Views
{
    public class DebtsSummaryModel : ObservableObject
    {
        private int _id;
        private string _serviceName = null!;
        private string _description = null!;
        private string _notes = null!;
        private decimal? _totalDebts;
        private string _status = null!;


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

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public decimal? TotalDebts
        {
            get => _totalDebts;
            set => SetProperty(ref _totalDebts, value);
        }

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
    }
}
