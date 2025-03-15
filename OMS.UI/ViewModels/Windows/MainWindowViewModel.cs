using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.Navigation;
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
        private readonly IUserSessionService _userSessionService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private UserModel? _currentUser;

        public MainWindowViewModel(INavigationService navigationService, IWindowService windowService,
                                   IUserSessionService userSessionService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _windowService = windowService;
            _userSessionService = userSessionService;
            _dialogService = dialogService;

            CurrentUser = _userSessionService.CurrentUser;

            WeakReferenceMessenger.Default.Register<IMessage<UserModel>>(this, (r, m) => { CurrentUser = m.Model; });
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
                _dialogService.ShowDialog<AddEditUserWindow>(userId);
        }

        [RelayCommand]
        private void Logout()
        {
            _userSessionService.Logout();
            _windowService.Close();
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
            OpenPeopleCommand.NotifyCanExecuteChanged();
            OpenUsersCommand.NotifyCanExecuteChanged();
            OpenBranchesCommand.NotifyCanExecuteChanged();
        }
    }
}
