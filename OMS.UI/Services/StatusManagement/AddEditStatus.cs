namespace OMS.UI.Services.StatusManagement
{
    public class AddEditStatus : BaseStatus
    {
        public enum EnExecuteOperation { Empty, Added, Updated }
        public enum EnMode { Empty, Add, Edit }

        private EnMode _selectMode;
        private string _title = "نمط العملية";
        private string _color = "#FF4682B4";
        private EnExecuteOperation _operation;

        public AddEditStatus()
        {
            ClickContent = "حفظ";
            IsModifiable = true;
        }

        public EnMode SelectMode
        {
            get => _selectMode;
            set
            {
                if (SetProperty(ref _selectMode, value))
                {
                    UpdateProperties();
                }
            }
        }

        public string? Title
        {
            get => _title;
            private set => SetProperty(ref _title!, value);
        }

        public string? Color
        {
            get => _color;
            private set => SetProperty(ref _color!, value);
        }

        public EnExecuteOperation Operation
        {
            get => _operation;
            set
            {
                UpdateClickContent();
                SetProperty(ref _operation, value);
                UpdateModifiableStatus();
            }
        }

        private void UpdateClickContent()
        {
            if (SelectMode == EnMode.Add)
            {
                SelectMode = EnMode.Edit;
                ClickContent = "تم الأضافة";
            }
            else
            {
                ClickContent = "تم التعديل";
            }
        }

        private void UpdateModifiableStatus()
        {
            IsModifiable = !(Operation == EnExecuteOperation.Added || Operation == EnExecuteOperation.Updated);
        }

        private void UpdateProperties()
        {
            switch (SelectMode)
            {
                case EnMode.Add:
                    Title = "نمط الإضافة";
                    Color = "#FF4682B4";
                    ClickContent = "أضافة";
                    break;

                case EnMode.Edit:
                    Title = "نمط التعديل";
                    Color = "#FFFF8C00";
                    ClickContent = "تعديل";
                    break;
            }
        }
    }
}
