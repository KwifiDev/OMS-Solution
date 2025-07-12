namespace OMS.UI.Services.Loading
{
    public interface ILoadingService
    {
        bool IsLoading { get; set; }
        Task ExecuteWithLoadingIndicator(Func<Task> action, int delayThreshold = 500);
    }
}
