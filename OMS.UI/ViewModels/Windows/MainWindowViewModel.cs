using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.Common.Data;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
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

        [ObservableProperty]
        private MainButtonsVisibilityModel _mainButtonVisibility = new();

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

            _userSessionService.ClaimsChanged += NotifyCurrentUserClaimsChanged;

        }

        [RelayCommand]
        private async Task LoadData()
        {
            if (!_userSessionService.IsLoggedIn) _windowService.CloseMainWindow();
            await _navigationService.NavigateToPageAsync<WelcomePage>();

            NotifyCommandsCanExecuteChanged();
            LoadButtonVisibility();
        }

        private void NotifyCurrentUserClaimsChanged()
        {
            NotifyCommandsCanExecuteChanged();
            LoadButtonVisibility();
        }

        private void LoadButtonVisibility()
        {
            MainButtonVisibility.Dashboard = CheckUserClaim(PermissionsData.Dashboard.View);
            MainButtonVisibility.Revenues = CheckUserClaim(PermissionsData.Revenues.View);
            MainButtonVisibility.People = CheckUserClaim(PermissionsData.PeopleDetail.View);
            MainButtonVisibility.Users = CheckUserClaim(PermissionsData.UsersDetail.View);
            MainButtonVisibility.Branches = CheckUserClaim(PermissionsData.BranchesOperationalMetric.View);
            MainButtonVisibility.Services = CheckUserClaim(PermissionsData.ServicesSummary.View);
            MainButtonVisibility.Clients = CheckUserClaim(PermissionsData.ClientsSummary.View);
            MainButtonVisibility.Settings = CheckUserClaim(PermissionsData.RolesSummary.View);
        }



        [RelayCommand]
        private async Task EditUser()
        {
            int? userId = _userSessionService.CurrentUser?.Id;

            if (userId != null && userId > 0)
                await _dialogService.ShowDialog<AddEditUserWindow, (int? UserId, bool IsOpendOnUsersPage)>((userId, false));
        }

        [RelayCommand]
        private void ChangeUserPassword()
        {
            int? userId = _userSessionService.CurrentUser?.Id;

            if (userId != null && userId > 0)
                _dialogService.ShowDialog<ChangePasswordWindow, int?>(userId);
        }

        [RelayCommand]
        private void Logout()
        {
            var ok = _messageService.ShowQuestionMessage("تسجيل خروج", MessageTemplates.LogoutConfirmation);
            if (!ok) return;

            ResetViewModelToDefault();
            _userSessionService.Logout();
            _windowService.CloseMainWindow();
            _windowService.Open<LoginWindow>();
        }

        private void ResetViewModelToDefault()
        {
            CurrentUser = null;
            _navigationService.ResetCurrentSelectedPage();
            NotifyCommandsCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanOpenDashboard))]
        private async Task OpenDashboard()
        {
            await _navigationService.NavigateToPageAsync<DashboardPage>();
            NotifyCommandsCanExecuteChanged();
        }
        private bool CanOpenDashboard()
        {
            return CanOpenPage<DashboardPageViewModel>(PermissionsData.Dashboard.View);
        }

        [RelayCommand(CanExecute = nameof(CanOpenRevenues))]
        private async Task OpenRevenues()
        {
            await _navigationService.NavigateToPageAsync<RevenuesPage>();
            NotifyCommandsCanExecuteChanged();
        }

        private bool CanOpenRevenues()
        {
            return CanOpenPage<RevenuesPageViewModel>(PermissionsData.Revenues.View);
        }


        [RelayCommand(CanExecute = nameof(CanOpenPeople))]
        private async Task OpenPeople()
        {
            await _navigationService.NavigateToPageAsync<PeoplePage>();
            NotifyCommandsCanExecuteChanged();
        }
        private bool CanOpenPeople()
        {
            return CanOpenPage<PeoplePageViewModel>(PermissionsData.PeopleDetail.View);
        }


        [RelayCommand(CanExecute = nameof(CanOpenUsers))]
        private async Task OpenUsers()
        {
            await _navigationService.NavigateToPageAsync<UsersPage>();
            NotifyCommandsCanExecuteChanged();
        }
        private bool CanOpenUsers()
        {
            return CanOpenPage<UsersPageViewModel>(PermissionsData.UsersDetail.View);
        }


        [RelayCommand(CanExecute = nameof(CanOpenBranches))]
        private async Task OpenBranches()
        {
            await _navigationService.NavigateToPageAsync<BranchesPage>();
            NotifyCommandsCanExecuteChanged();
        }
        private bool CanOpenBranches()
        {
            return CanOpenPage<BranchesPageViewModel>(PermissionsData.BranchesOperationalMetric.View);
        }

        [RelayCommand(CanExecute = nameof(CanOpenServices))]
        private async Task OpenServices()
        {
            await _navigationService.NavigateToPageAsync<ServicesPage>();
            NotifyCommandsCanExecuteChanged();
        }

        private bool CanOpenServices()
        {
            return CanOpenPage<ServicesPageViewModel>(PermissionsData.ServicesSummary.View);
        }

        [RelayCommand(CanExecute = nameof(CanOpenClients))]
        private async Task OpenClients()
        {
            await _navigationService.NavigateToPageAsync<ClientsPage>();
            NotifyCommandsCanExecuteChanged();
        }

        private bool CanOpenClients()
        {
            return CanOpenPage<ClientsPageViewModel>(PermissionsData.ClientsSummary.View);
        }

        [RelayCommand(CanExecute = nameof(CanOpenSettings))]
        private async Task OpenSettings()
        {
            await _navigationService.NavigateToPageAsync<SettingsPage>();
            NotifyCommandsCanExecuteChanged();
        }

        private bool CanOpenSettings()
        {
            return CanOpenPage<SettingsPageViewModel>(PermissionsData.Roles.View);
        }


        [RelayCommand]
        private void Minimize() => _windowService.Minimize();


        [RelayCommand]
        private void Maximize() => _windowService.Maximize();


        [RelayCommand]
        private void Exit() 
        {
            if (!_messageService.ShowQuestionMessage("الخروج من التطبيق", "هل انت متاكد من انك تريد الخروج من التطبيق")) return;
            _windowService.Exit();
        } 

        private bool CanOpenPage<TViewModel>(string claim) where TViewModel : class
        {
            return _navigationService.SelectedViewModelPage?.GetType() != typeof(TViewModel) && CheckUserClaim(claim) && CurrentUser != null;
        }

        private bool CheckUserClaim(string claim)
            => _userSessionService.Claims!.Contains(claim);


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
