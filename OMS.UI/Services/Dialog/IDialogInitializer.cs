namespace OMS.UI.Services.Dialog
{
    public interface IDialogInitializer<TParam>
    {
        Task<bool> OnOpeningDialog(TParam? parameters);
    }
}
