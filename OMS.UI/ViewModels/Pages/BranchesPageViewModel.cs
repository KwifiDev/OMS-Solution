using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class BranchesPageViewModel : BasePageViewModel<IBranchService, IBranchOperationalMetricService, BranchOperationalMetricModel, BranchModel>
    {
        public BranchesPageViewModel(IBranchService branchService, IBranchOperationalMetricService branchOperationalMetricService,
                                     IDialogService dialogService, IMessageService messageService)
                                     : base(branchService, branchOperationalMetricService, dialogService, messageService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(BranchOperationalMetricModel item)
            => item.BranchId;

        protected override async Task LoadData()
        {
            var branchItems = await _displayService.GetAllAsync();
            Items = new(branchItems);
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            throw new NotImplementedException();
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditBranchWindow, int?>(itemId);

        protected override async Task<BranchOperationalMetricModel> ConvertToModel(BranchModel messageModel)
        {
            return (await _displayService.GetByIdAsync(messageModel.BranchId))!;
        }
    }
}
