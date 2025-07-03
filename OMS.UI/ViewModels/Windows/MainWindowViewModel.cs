using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Navigation;
using OMS.UI.Services.Registry;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Pages;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IWindowService _windowService;
        private readonly IUserService _userService;
        private readonly IRegistryService _registryService;
        private readonly IUserSessionService _userSessionService;
        private readonly IDialogService _dialogService;
        private readonly IMessageService _messageService;

        [ObservableProperty]
        private UserLoginModel? _currentUser;

        public MainWindowViewModel(INavigationService navigationService, IWindowService windowService, IUserService userService, IRegistryService registryService,
                                   IUserSessionService userSessionService, IDialogService dialogService, IMessageService messageService)
        {
            _navigationService = navigationService;
            _windowService = windowService;
            _userService = userService;
            _registryService = registryService;
            _userSessionService = userSessionService;
            _dialogService = dialogService;
            _messageService = messageService;

            CurrentUser = _userSessionService.CurrentUser;

            WeakReferenceMessenger.Default.Register<UserLoginModel>(this, (r, user) => CurrentUser = user);
        }

        [RelayCommand]
        private async Task LoadData()
        {
            await OpenDashboard();
        }

        [RelayCommand]
        private void EditUser()
        {
            int? userId = _userSessionService.CurrentUser?.UserId;

            if (userId != null && userId > 0)
                _dialogService.ShowDialog<AddEditUserWindow, int?>(userId);
        }

        [RelayCommand]
        private void Logout()
        {
            var ok = _messageService.ShowQuestionMessage("تسجيل خروج", MessageTemplates.LogoutConfirmation);
            if (!ok) return;

            _userSessionService.Logout();
            _registryService.ResetUserLoginConfig();
            _windowService.Hide();
            _windowService.Open<LoginWindow>();
        }

        [RelayCommand(CanExecute = nameof(CanOpenDashboard))]
        private async Task OpenDashboard()
        {
            await _navigationService.NavigateToPageAsync<DashboardPage>();
            NotifyCommandsCanExecuteChanged();
        }
        private bool CanOpenDashboard()
        {
            return CanOpenPage<DashboardPageViewModel>();
        }

        [RelayCommand(CanExecute = nameof(CanOpenRevenues))]
        private async Task OpenRevenues()
        {
            await _navigationService.NavigateToPageAsync<RevenuesPage>();
            NotifyCommandsCanExecuteChanged();
        }

        private bool CanOpenRevenues()
        {
            return CanOpenPage<RevenuesPageViewModel>();
        }


        [RelayCommand(CanExecute = nameof(CanOpenPeople))]
        private async Task OpenPeople()
        {
            await _navigationService.NavigateToPageAsync<PeoplePage>();
            NotifyCommandsCanExecuteChanged();
        }
        private bool CanOpenPeople()
        {
            return CanOpenPage<PeoplePageViewModel>();
        }


        [RelayCommand(CanExecute = nameof(CanOpenUsers))]
        private async Task OpenUsers()
        {
            await _navigationService.NavigateToPageAsync<UsersPage>();
            NotifyCommandsCanExecuteChanged();
        }
        private bool CanOpenUsers()
        {
            return CanOpenPage<UsersPageViewModel>();
        }


        [RelayCommand(CanExecute = nameof(CanOpenBranches))]
        private async Task OpenBranches()
        {
            await _navigationService.NavigateToPageAsync<BranchesPage>();
            NotifyCommandsCanExecuteChanged();
        }
        private bool CanOpenBranches()
        {
            return CanOpenPage<BranchesPageViewModel>();
        }

        [RelayCommand(CanExecute = nameof(CanOpenServices))]
        private async Task OpenServices()
        {
            await _navigationService.NavigateToPageAsync<ServicesPage>();
            NotifyCommandsCanExecuteChanged();
        }

        private bool CanOpenServices()
        {
            return CanOpenPage<ServicesPageViewModel>();
        }

        [RelayCommand(CanExecute = nameof(CanOpenClients))]
        private async Task OpenClients()
        {
            await _navigationService.NavigateToPageAsync<ClientsPage>();
            NotifyCommandsCanExecuteChanged();
        }

        private bool CanOpenClients()
        {
            return CanOpenPage<ClientsPageViewModel>();
        }

        [RelayCommand(CanExecute = nameof(CanOpenSettings))]
        private async Task OpenSettings()
        {
            await _navigationService.NavigateToPageAsync<SettingsPage>();
            NotifyCommandsCanExecuteChanged();
        }

        private bool CanOpenSettings()
        {
            return CanOpenPage<SettingsPageViewModel>();
        }


        [RelayCommand]
        private void Minimize() => _windowService.Minimize();


        [RelayCommand]
        private void Maximize() => _windowService.Maximize();


        [RelayCommand]
        private void Exit() => _windowService.Exit();

        private bool CanOpenPage<TViewModel>() where TViewModel : class
        {
            return _navigationService.SelectedViewModelPage?.GetType() != typeof(TViewModel);
        }

        private void NotifyCommandsCanExecuteChanged()
        {
            OpenDashboardCommand.NotifyCanExecuteChanged();
            OpenRevenuesCommand.NotifyCanExecuteChanged();
            OpenPeopleCommand.NotifyCanExecuteChanged();
            OpenUsersCommand.NotifyCanExecuteChanged();
            OpenBranchesCommand.NotifyCanExecuteChanged();
            OpenServicesCommand.NotifyCanExecuteChanged();
            OpenClientsCommand.NotifyCanExecuteChanged();
            OpenSettingsCommand.NotifyCanExecuteChanged();
        }
    }
}
