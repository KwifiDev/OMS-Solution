using OMS.Common.Data;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Views;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class BranchesPageViewModel : BasePageViewModel<IBranchService, IBranchOperationalMetricService, BranchOperationalMetricModel, BranchModel>
    {

        protected override string ViewClaim => PermissionsData.BranchesOperationalMetric.View;
        protected override string AddClaim => PermissionsData.Branches.Add;
        protected override string EditClaim => PermissionsData.Branches.Edit;
        protected override string DeleteClaim => PermissionsData.Branches.Delete;

        public BranchesPageViewModel(IBranchService branchService, IBranchOperationalMetricService branchOperationalMetricService, 
                                     ILoadingService loadingService, IDialogService dialogService, IMessageService messageService, IUserSessionService userSessionService)
                                     : base(branchService, branchOperationalMetricService, loadingService, dialogService, messageService, userSessionService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(BranchOperationalMetricModel item)
            => item.BranchId;

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditBranchWindow, int?>(itemId);

        protected override async Task<BranchOperationalMetricModel> ConvertToModel(BranchModel messageModel)
        {
            return (await _displayService.GetByIdAsync(messageModel.BranchId))!;
        }
    }
}
