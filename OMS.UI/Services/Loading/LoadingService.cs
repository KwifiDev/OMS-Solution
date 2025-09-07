using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Services.Loading
{
    public partial class LoadingService : ObservableObject, ILoadingService
    {
        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _isWaiting;

        public async Task ExecuteWithLoadingIndicator(Func<Task> action, int delayThreshold = 500)
        {
            IsWaiting = false;

            var loadingDelayTask = Task.Delay(delayThreshold);
            var actionTask = action();

            var completedTask = await Task.WhenAny(loadingDelayTask, actionTask);

            if (completedTask == loadingDelayTask)
            {
                IsLoading = true;
            }

            await actionTask;
            IsLoading = false;

            IsWaiting = true;
        }
        
    }
}
