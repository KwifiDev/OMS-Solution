using System.Windows;

namespace OMS.UI.Services.Dialog
{
    public interface IDialogService
    {
        Task<bool> ShowDialog<TWindow, TParam>(TParam? parameters = default)
        where TWindow : Window;
    }
}
