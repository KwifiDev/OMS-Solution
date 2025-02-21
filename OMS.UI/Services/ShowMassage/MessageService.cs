using System.Windows;

namespace OMS.UI.Services.ShowMassage
{
    public class MessageService : IMessageService
    {

        public void ShowErrorMessage(string caption, string message)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfoMessage(string caption, string message)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool ShowQuestionMessage(string caption, string message)
        {
            var msg = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return msg == MessageBoxResult.Yes;
        }

        public bool ShowWarningMessage(string caption, string message)
        {
            var msg = MessageBox.Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            return msg == MessageBoxResult.OK;

        }
    }
}
