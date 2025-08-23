using System.Windows.Controls;

namespace OMS.UI.Services.Navigation
{
    public interface INavigationService
    {
        void SetFrame(Frame frame);
        object? SelectedViewModelPage { get; }
        Task NavigateToPageAsync<T>() where T : Page;
        Task ClearHistoryAsync();
        Task ResetCurrentSelectedPage();
    }
}