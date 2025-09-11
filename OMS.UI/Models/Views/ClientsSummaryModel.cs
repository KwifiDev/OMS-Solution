using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;

namespace OMS.UI.Models.Views
{
    public class ClientsSummaryModel : ObservableObject
    {
        private int _id;
        private int? _accountId;
        private string _fullName = null!;
        private string? _phone;
        private EnClientType _clientType;


        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int? AccountId
        {
            get => _accountId;
            set => SetProperty(ref _accountId, value);
        }

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        public string? Phone
        {
            get => _phone;
            set 
            {
                SetProperty(ref _phone, value);
                OnPropertyChanged(nameof(PhoneDisplay));
            } 
        }

        public EnClientType ClientType
        {
            get => _clientType;
            set 
            {
                SetProperty(ref _clientType, value);
                OnPropertyChanged(nameof(ClientTypeDisplay));
            }
        }

        // Display Props

        public string PhoneDisplay => Phone ?? "لا يوجد رقم هاتف";

        public string ClientTypeDisplay => ClientType == EnClientType.Normal ? "عادي" :
                                           ClientType == EnClientType.Lawyer ? "محامي" : "أخرى";
    }
}
