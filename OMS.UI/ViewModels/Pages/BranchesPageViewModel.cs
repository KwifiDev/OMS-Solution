using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Pages
{
    public partial class BranchesPageViewModel : ObservableObject
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;

        [ObservableProperty]
        private ObservableCollection<BranchModel> _branches = null!;

        [ObservableProperty]
        private BranchModel? _selectedBranch;

        public BranchesPageViewModel(IBranchService branchService, IMapper mapper)
        {
            _branchService = branchService;
            _mapper = mapper;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            var branchesDto = await _branchService.GetAllAsync();
            var branches = _mapper.Map<IEnumerable<BranchModel>>(branchesDto);
            Branches = new ObservableCollection<BranchModel>(branches ?? Enumerable.Empty<BranchModel>());
        }
    }
}
