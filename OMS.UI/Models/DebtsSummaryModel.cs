using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Models
{
    public class DebtsSummaryModel : ObservableObject
    {
        private int _debtId;
        private string _serviceName = null!;
        private string _description = null!;
        private string _notes = null!;
        private string? _totalDebts;
        private string _status = null!;


        public int DebtId
        {
            get => _debtId;
            set => SetProperty(ref _debtId, value);
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

        public string? TotalDebts
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
