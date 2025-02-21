namespace OMS.UI.ViewModels.Interfaces
{
    public interface IViewModelInitializer
    {
        Task<bool> Initialize(int? id);
    }
}
