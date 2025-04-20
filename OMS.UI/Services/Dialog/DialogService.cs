using CommunityToolkit.Mvvm.DependencyInjection;
using System.Windows;

namespace OMS.UI.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public async Task<bool> ShowDialog<TWindow, TParam>(TParam? parameters = default)
            where TWindow : Window
        {
            var window = Ioc.Default.GetRequiredService<TWindow>();

            if (window.DataContext is IDialogInitializer<TParam> viewModel)
            {
                bool isSuccess = false;
                try
                {
                    isSuccess = await viewModel.OnOpeningDialog(parameters);
                }
                catch (Exception ex)
                {
                    HandleInitializationError(ex);
                }

                if (isSuccess)
                {
                    window.ShowDialog();
                    return true;
                }
            }
            return false;
        }

        private void HandleInitializationError(Exception ex)
        {
            // You can add additional instructions here to handle specific types of errors.
        }
    }
}
