using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Models
{
    public class BranchModel : ObservableValidator
    {
        private int _branchId;
        private string _name = null!;
        private string _address = null!;


        public int BranchId
        {
            get => _branchId;
            set
            {
                SetProperty(ref _branchId, value);
                OnPropertyChanged(nameof(BranchIdDisplay));
            }
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        // DisplayProperty
        public string BranchIdDisplay => _branchId > 0 ? _branchId.ToString() : "لا يوجد";

        public bool ArePropertiesValid()
        {
            ValidateAllProperties();
            return !HasErrors;
        }
    }
}
