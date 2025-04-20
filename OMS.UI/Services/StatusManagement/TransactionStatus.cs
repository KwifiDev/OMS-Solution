namespace OMS.UI.Services.StatusManagement
{
    public class TransactionStatus : BaseStatus
    {
        public enum EnExecuteOperation { Empty, Deposited, Withdrawn, Transferred }
        public enum EnMode { Empty, Deposit, Withdraw, Transfer }

        private EnMode _selectMode;
        private string _title = "نمط العملية";
        private string _color = "#FF4682B4";
        private EnExecuteOperation _operation;

        public TransactionStatus()
        {
            ClickContent = "تحويل";
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
            switch (SelectMode)
            {
                case EnMode.Deposit:
                    ClickContent = "تم الإيداع";
                    break;

                case EnMode.Withdraw:
                    ClickContent = "تم السحب";
                    break;

                case EnMode.Transfer:
                    ClickContent = "تم التحويل";
                    break;
            }
        }

        private void UpdateModifiableStatus()
        {
            IsModifiable = Operation == EnExecuteOperation.Empty;
        }

        private void UpdateProperties()
        {
            switch (SelectMode)
            {
                case EnMode.Deposit:
                    SetPropertiesValues("عملية إيداع", "#FF4682B4", "إيداع");
                    break;

                case EnMode.Withdraw:
                    SetPropertiesValues("عملية سحب", "#E81123", "سحب");
                    break;

                case EnMode.Transfer:
                    SetPropertiesValues("عملية تحويل", "#FFFF8C00", "تحويل");
                    break;
            }
        }

        private void SetPropertiesValues(string title, string color, string clickContent)
        {
            Title = title;
            Color = color;
            ClickContent = clickContent;
        }
    }
}
