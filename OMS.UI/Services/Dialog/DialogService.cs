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
                try
                {
                    bool isSuccess = await viewModel.OnOpeningDialog(parameters);
                    if (isSuccess)
                    {
                        window.ShowDialog();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    HandleInitializationError(ex);
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
