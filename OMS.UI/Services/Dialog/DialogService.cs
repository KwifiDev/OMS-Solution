using CommunityToolkit.Mvvm.DependencyInjection;
using OMS.UI.ViewModels.Interfaces;
using System.Windows;

namespace OMS.UI.Services.Dialog
{
    public class DialogService : IDialogService
    {
        private async Task<TViewModel?> ShowDialogInternal<TWindow, TViewModel>(int? id)
        where TWindow : Window
        where TViewModel : class, IViewModelInitializer
        {
            var window = Ioc.Default.GetRequiredService<TWindow>();
            if (window.DataContext is TViewModel viewModel)
            {
                bool isSuccess = await viewModel.Initialize(id);
                if (isSuccess)
                {
                    window.ShowDialog();
                    return viewModel;
                }
            }
            return null;
        }

        public async Task<bool> ShowDialog<TWindow, TViewModel>(int? id = null)
        where TWindow : Window
        where TViewModel : class, IViewModelInitializer
        {
            var viewModel = await ShowDialogInternal<TWindow, TViewModel>(id);
            return viewModel != null;
        }
    }
}
