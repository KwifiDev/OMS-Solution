namespace OMS.UI.Services.Windows
{
    public interface IWindowService
    {
        void Close();
        void Minimize();
        void Maximize();
        void DragMove();
    }
}
