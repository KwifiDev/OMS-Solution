namespace OMS.UI.Services.ShowMassage
{
    public interface IMessageService
    {
        void ShowInfoMessage(string caption, string message);
        void ShowErrorMessage(string caption, string message);
        bool ShowWarningMessage(string caption, string message);
        bool ShowQuestionMessage(string caption, string message);

    }
}
