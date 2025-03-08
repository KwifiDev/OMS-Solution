using AutoMapper;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditBranchViewModel : AddEditBaseViewModel<BranchModel, BranchDto, IBranchService>
    {

        public AddEditBranchViewModel(IBranchService branchService, IMapper mapper, IMessageService messageService,
                                      IWindowService windowService, IStatusService statusService)
                                      : base(branchService, mapper, messageService, windowService, statusService)
        {
        }

        protected override async Task<BranchDto?> GetByIdAsync(int branchId)
            => await _service.GetByIdAsync(branchId);

        protected override string GetEntityName()
            => "فرع";

        protected override async Task<bool> SaveDataAsync(bool isAdding, BranchDto branchDto)
            => isAdding ? await _service.AddAsync(branchDto) : await _service.UpdateAsync(branchDto);

        protected override void UpdateModelAfterSave(BranchDto branchDto)
            => Model.BranchId = branchDto.BranchId;
    }
}
