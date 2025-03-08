namespace OMS.UI.Services.Dialog
{
    public interface IDialogInitializer
    {
        Task<bool> OnOpeningDialog(int? id);
    }
}
