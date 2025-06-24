namespace OMS.UI.Services.StatusManagement
{
    public class DebtStatus : BaseStatus
    {
        public enum EnExecuteOperation { Empty, FullPaid }
        public enum EnMode { Empty, PaySpecifiecDebt, PayAllDebtsByClientId }

        private EnMode _selectMode;
        private EnExecuteOperation _operation;
        private string _title = "نمط العملية";
        private string _color = "#FF4682B4";

        public DebtStatus()
        {
            ClickContent = "دفع";
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
                ClickContent = "تم الدفع";
                SetProperty(ref _operation, value);
                UpdateModifiableStatus();
            }
        }

        private void UpdateModifiableStatus()
        {
            IsModifiable = !(Operation == EnExecuteOperation.FullPaid);
        }


        private void UpdateProperties()
        {
            switch (SelectMode)
            {
                case EnMode.PaySpecifiecDebt:
                    SetPropertiesValues("عملية دفع دين محدد", "#FF4682B4");
                    break;

                case EnMode.PayAllDebtsByClientId:
                    SetPropertiesValues("عملية دفع جميع ديون العميل", "#E81123");
                    break;
            }
        }

        private void SetPropertiesValues(string title, string color)
        {
            Title = title;
            Color = color;
        }
    }
}
