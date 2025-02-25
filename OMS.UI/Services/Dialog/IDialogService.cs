using System.Windows;

namespace OMS.UI.Services.Dialog
{
    public interface IDialogService
    {
        Task<bool> ShowDialog<TWindow>(int? id = null)
        where TWindow : Window;
    }
}
