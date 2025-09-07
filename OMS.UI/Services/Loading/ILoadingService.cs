namespace OMS.UI.Services.Loading
{
    public interface ILoadingService
    {
        bool IsLoading { get; set; }
        bool IsWaiting { get; set; }
        Task ExecuteWithLoadingIndicator(Func<Task> action, int delayThreshold = 500);
    }
}
