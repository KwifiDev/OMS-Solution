using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class RevenuesPageViewModel : BasePageViewModel<IRevenueService, IRevenueService, RevenueModel, RevenueModel>
    {
        public RevenuesPageViewModel(IRevenueService revenueService, IDialogService dialogService, IMessageService messageService)
                                     : base(revenueService, revenueService, dialogService, messageService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(RevenueModel item)
            => item.RevenueId;

        protected override async Task LoadData()
        {
            var revenueItems = await _displayService.GetAllAsync();
            Items = new(revenueItems);
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditRevenueWindow, int?>(itemId);

        protected override async Task<RevenueModel> ConvertToModel(RevenueModel messageModel)
        {
            return (await _displayService.GetByIdAsync(messageModel.RevenueId))!;
        }
    }
}
