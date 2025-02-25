using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.Services.Navigation;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Pages;

namespace OMS.UI.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IWindowService _windowService;

        public MainWindowViewModel(INavigationService navigationService, IWindowService windowService)
        {
            _navigationService = navigationService;
            _windowService = windowService;
            _navigationService.NavigateToPageAsync<DashboardPage>();
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
        private void Minimize()
        {
            _windowService.Minimize();
        }

        [RelayCommand]
        private void Maximize()
        {
            _windowService.Maximize();
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }

        [RelayCommand]
        private void DragWindow()
        {
            _windowService.DragMove();
        }

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
