using OMS.UI.ViewModels.Interfaces;
using System.Windows;

namespace OMS.UI.Services.Dialog
{
    public interface IDialogService
    {
        Task<bool> ShowDialog<TWindow, TViewModel>(int? id = null)
        where TWindow : Window
        where TViewModel : class, IViewModelInitializer;
    }
}
