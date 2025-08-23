using CommunityToolkit.Mvvm.DependencyInjection;
using System.Windows.Controls;

namespace OMS.UI.Services.Navigation
{

    public class NavigationService : INavigationService
    {
        private Frame _mainFrame = null!;

        private Page? _currentSelectedPage;

        public object? SelectedViewModelPage => _currentSelectedPage?.DataContext;

        public void SetFrame(Frame mainFrame)
        {
            _mainFrame = mainFrame;
        }

        public async Task NavigateToPageAsync<T>() where T : Page
        {
            _currentSelectedPage = Ioc.Default.GetRequiredService<T>();
            _mainFrame.Navigate(_currentSelectedPage);
            await ClearHistoryAsync();
        }

        public async Task ClearHistoryAsync()
        {
            await Task.Run(() =>
            {
                _mainFrame.Dispatcher.Invoke(() =>
                {
                    while (_mainFrame.NavigationService.CanGoBack)
                    {
                        _mainFrame.NavigationService.RemoveBackEntry();
                    }
                });
            });
        }

        public async Task ResetCurrentSelectedPage()
        {
            _currentSelectedPage = null;
            _mainFrame.Navigate(_currentSelectedPage);
            await ClearHistoryAsync();
        }

    }
}
