using CommunityToolkit.Mvvm.Input;
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
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class SalesSummaryViewModel : BasePageViewModel<ISaleService, ISalesSummaryService, SalesSummaryModel, SaleModel>, IDialogInitializer<int?>
    {
        protected override string ViewClaim => PermissionsData.SalesSummary.View;
        protected override string AddClaim => PermissionsData.Sales.Add;
        protected override string EditClaim => PermissionsData.Sales.Edit;
        protected override string DeleteClaim => PermissionsData.Sales.Delete;

        private readonly IWindowService _windowService;
        private int _clientId;

        public SalesSummaryViewModel(ISaleService service, ISalesSummaryService displayService, ILoadingService loadingService,
                                     IDialogService dialogService, IMessageService messageService, IWindowService windowService, IUserSessionService userSessionService)
                                     : base(service, displayService, loadingService, dialogService, messageService, userSessionService)
        {
            SelectedItemChanged += NotifyCanExecuteChanged;
            CommandConditions[nameof(EditItemCommand)] += CanChangeSale;
            CommandConditions[nameof(DeleteItemCommand)] += CanChangeSale;
            _windowService = windowService;
        }

        private void NotifyCanExecuteChanged(object? obj, EventArgs e)
        {
            CancelSaleCommand.NotifyCanExecuteChanged();
        }

        public async Task<bool> OnOpeningDialog(int? clientId)
        {
            if (clientId <= 0) return false;

            _clientId = (int)clientId!;
            await LoadData();

            return true;
        }

        protected override async Task<SalesSummaryModel> ConvertToModel(SaleModel messageModel)
            => (await _displayService.GetByIdAsync(messageModel.SaleId))!;

        protected override async Task<bool> ExecuteDelete(int itemId) => await _service.DeleteAsync(itemId);

        protected override int GetItemId(SalesSummaryModel item) => item.SaleId;

        protected override async Task LoadData()
        {
            await LoadingService.ExecuteWithLoadingIndicator(async () =>
            {
                var salesData = await _displayService.GetSalesByClientIdAsync(_clientId);
                Items = new(salesData);
            });
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditSaleWindow, (int? SaleId, int ClientId)>((itemId, _clientId));

        [RelayCommand(CanExecute = nameof(CanChangeSale))]
        private async Task CancelSale()
        {
            if (SelectedItem is null) return;

            if (!_messageService.ShowQuestionMessage("تحذير", MessageTemplates.CancellationSaleConfirmation))
                return;

            var isSuccess = await _service.CancelSaleAsync(SelectedItem.SaleId);

            if (!isSuccess)
            {
                _messageService.ShowErrorMessage("عملية الالغاء", MessageTemplates.CancelSaleErrorMessage);
                return;
            }

            SelectedItem.Status = "ملغات";
            _messageService.ShowInfoMessage("عملية الالغاء", MessageTemplates.CancelSaleSuccessMessage);
        }

        [RelayCommand]
        private void Close() => _windowService.Close();

        private bool CanChangeSale()
        {
            return SelectedItem?.Status == "غير مكتملة";
        }
    }
}
