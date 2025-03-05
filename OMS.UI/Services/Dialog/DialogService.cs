using CommunityToolkit.Mvvm.DependencyInjection;
using System.Windows;

namespace OMS.UI.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public async Task<bool> ShowDialog<TWindow>(int? id = null)
        where TWindow : Window
        {
            var viewModel = await ShowDialogInternal<TWindow, IDialogInitializer>(id);
            return viewModel != null;
        }

        private async Task<TViewModel?> ShowDialogInternal<TWindow, TViewModel>(int? id)
        where TWindow : Window
        where TViewModel : class, IDialogInitializer
        {
            var window = Ioc.Default.GetRequiredService<TWindow>();
            return await InitializeAndShowDialog<TWindow, TViewModel>(window, id);
        }

        private async Task<TViewModel?> InitializeAndShowDialog<TWindow, TViewModel>(TWindow window, int? id)
        where TWindow : Window
        where TViewModel : class, IDialogInitializer
        {
            if (window.DataContext is TViewModel viewModel)
            {
                bool isSuccess = false;
                try
                {
                    isSuccess = await viewModel.Initialize(id);
                }
                catch (Exception ex)
                {
                    HandleInitializationError(ex);
                }

                if (isSuccess)
                {
                    window.ShowDialog();
                    return viewModel;
                }
            }
            return null;
        }

        private void HandleInitializationError(Exception ex)
        {
            // You can add additional instructions here to handle specific types of errors.
        }
    }
}
