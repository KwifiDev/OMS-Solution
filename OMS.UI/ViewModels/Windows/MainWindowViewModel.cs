using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.Services.Navigation;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace OMS.UI.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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


        [RelayCommand]
        private void Minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        [RelayCommand]
        private void Maximize()
        {
            Window window = Application.Current.MainWindow;
            window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        [RelayCommand]
        private void Close()
        {
            Application.Current.MainWindow.Close();
        }

        [RelayCommand]
        private void DragWindow(Window window)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                window.DragMove();
            }
        }

        private bool CanOpenPage<TViewModel>() where TViewModel : class
        {
            return _navigationService.SelectedViewModelPage?.GetType() != typeof(TViewModel);
        }

        private void NotifyCommandsCanExecuteChanged()
        {
            OpenDashboardCommand.NotifyCanExecuteChanged();
            OpenPeopleCommand.NotifyCanExecuteChanged();
        }


    }
}
