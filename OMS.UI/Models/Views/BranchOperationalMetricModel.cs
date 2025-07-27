using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class BranchOperationalMetricModel : ObservableObject
    {
        private int _branchId;
        private string _name = null!;
        private string _address = null!;
        private int? _totalEmployees;

        [Key]
        public int BranchId
        {
            get => _branchId;
            set => SetProperty(ref _branchId, value);
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

        public int? TotalEmployees
        {
            get => _totalEmployees;
            set => SetProperty(ref _totalEmployees, value);
        }
    }
}
