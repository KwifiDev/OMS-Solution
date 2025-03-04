using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Services.StatusManagement
{
    public abstract class BaseStatus : ObservableObject
    {
        private string? _clickContent;
        private bool _isModifiable;
        private object? _modelObject;

        public string? ClickContent
        {
            get => _clickContent;
            set => SetProperty(ref _clickContent, value);
        }

        public bool IsModifiable
        {
            get => _isModifiable;
            set => SetProperty(ref _isModifiable, value);
        }

        public object? ModelObject
        {
            get => _modelObject;
            set => SetProperty(ref _modelObject, value);
        }

        public bool IsReadOnly => !IsModifiable;
    }
}
