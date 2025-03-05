namespace OMS.UI.Services.Dialog
{
    public interface IDialogInitializer
    {
        Task<bool> Initialize(int? id);
    }
}
