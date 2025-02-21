using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Services.Status
{
    public class StatusInfo : ObservableObject
    {
        public enum EnExecuteOperation { Added, Updated }
        public enum EnMode { Add, Edit }

        public StatusInfo()
        {
            Title = "اضافة/تعديل";
            ClickContent = "حفظ";
            Color = SelectMode == EnMode.Add ? "#FF4682B4" : "#FFFF8C00";
            IsInChangeMode = true;
        }


        private EnExecuteOperation _operation;
        private string? _title;
        private string? _btnContent;
        private string? _color;
        private EnMode _selectMode;
        private bool _isInChangeMode;

        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string? ClickContent
        {
            get => _btnContent;
            set => SetProperty(ref _btnContent, value);
        }

        public string? Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public EnMode SelectMode
        {
            get => _selectMode;
            set
            {
                if (SetProperty(ref _selectMode, value))
                {
                    Color = _selectMode == EnMode.Add ? "#FF4682B4" : "#FFFF8C00";
                }
            }
        }

        public bool IsInChangeMode
        {
            get => _isInChangeMode;
            set => SetProperty(ref _isInChangeMode, value);
        }

        public EnExecuteOperation Operation
        {
            get => _operation;
            set => SetProperty(ref _operation, value);
        }

        public bool IsUpdated => !IsInChangeMode;

        public object? SavedObject { get; set; }
    }
}
