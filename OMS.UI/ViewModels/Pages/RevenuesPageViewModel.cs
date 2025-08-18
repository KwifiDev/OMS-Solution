using OMS.Common.Data;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class RevenuesPageViewModel : BasePageViewModel<IRevenueService, IRevenueService, RevenueModel, RevenueModel>
    {
        protected override string ViewClaim => PermissionsData.Revenues.View;
        protected override string AddClaim => PermissionsData.Revenues.Add;
        protected override string EditClaim => PermissionsData.Revenues.Edit;
        protected override string DeleteClaim => PermissionsData.Revenues.Delete;

        public RevenuesPageViewModel(IRevenueService revenueService, IDialogService dialogService, ILoadingService loadingService,
                                     IMessageService messageService, IUserSessionService userSessionService)
                                     : base(revenueService, revenueService, loadingService, dialogService, messageService, userSessionService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(RevenueModel item)
            => item.RevenueId;

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
        {
            if (itemId is null)
            {
                if (!await _service.CanAddOnThisDay())
                {
                    _messageService.ShowInfoMessage("منع اضافة", MessageTemplates.CantAddRevenueMessage);
                    return;
                }
            }


            await _dialogService.ShowDialog<AddEditRevenueWindow, int?>(itemId);
        }

        protected override async Task<RevenueModel> ConvertToModel(RevenueModel messageModel)
        {
            return (await _displayService.GetByIdAsync(messageModel.RevenueId))!;
        }
    }
}
