using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditBranchViewModel : AddEditBaseViewModel<BranchModel, IBranchService>
    {

        public AddEditBranchViewModel(IBranchService branchService, IMessageService messageService,
                                      IWindowService windowService, IStatusService statusService)
                                      : base(branchService, messageService, windowService, statusService)
        {
        }

        protected override async Task<BranchModel?> GetByIdAsync(int branchId)
            => await _service.GetByIdAsync(branchId);

        protected override string GetEntityName()
            => "فرع";

        protected override async Task<bool> SaveDataAsync(bool isAdding, BranchModel branchModel)
            => isAdding ? await _service.AddAsync(branchModel) : await _service.UpdateAsync(branchModel.BranchId, branchModel);

        //protected override void UpdateModelAfterSave(BranchModel branchModel)
        //    => Model.BranchId = branchModel.BranchId;
    }
}
