using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Services.StatusManagement
{
    public class SearchStatus : ObservableObject
    {
        public SearchStatus()
        {
            ClickContent = "بحث";
            IsInChangeMode = true;
        }


        private string? _clickContent;
        private bool _isInChangeMode;

        public string? ClickContent
        {
            get => _clickContent;
            set => SetProperty(ref _clickContent, value);
        }

        public bool IsInChangeMode
        {
            get => _isInChangeMode;
            set => SetProperty(ref _isInChangeMode, value);
        }

        public bool IsUpdated => !IsInChangeMode;

        public object? SavedObject { get; set; }
    }
}
