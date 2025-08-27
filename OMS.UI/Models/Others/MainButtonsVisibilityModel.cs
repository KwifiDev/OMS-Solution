using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Models.Others
{
    public partial class MainButtonsVisibilityModel : ObservableObject
    {

        [ObservableProperty]
        private bool _dashboard;

        [ObservableProperty]
        private bool _revenues;

        [ObservableProperty]
        private bool _people;

        [ObservableProperty]
        private bool _users;

        [ObservableProperty]
        private bool _branches;

        [ObservableProperty]
        private bool _services;

        [ObservableProperty]
        private bool _clients;

        [ObservableProperty]
        private bool _settings;
    }
}
