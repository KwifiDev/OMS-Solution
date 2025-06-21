using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class DebtsSummaryViewModel : BasePageViewModel<IDebtService, IDebtsSummaryService, DebtsSummaryModel, DebtModel>, IDialogInitializer<int?>
    {
        private readonly IWindowService _windowService;
        private int _clientId;

        public DebtsSummaryViewModel(IDebtService service, IDebtsSummaryService displayService, IDialogService dialogService,
                                     IMessageService messageService, IWindowService windowService) : base(service, displayService, dialogService, messageService)
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

        protected override async Task<DebtsSummaryModel> ConvertToModel(DebtModel messageModel)
            => (await _displayService.GetByIdAsync(messageModel.DebtId))!;

        protected override async Task<bool> ExecuteDelete(int itemId) => await _service.DeleteAsync(itemId);

        protected override int GetItemId(DebtsSummaryModel item) => item.DebtId;

        protected override async Task LoadData()
        {
            var debtsData = await _displayService.GetDebtsByClientIdAsync(_clientId);
            Items = new(debtsData);
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditDebtWindow, (int? DebtId, int ClientId)>((itemId, _clientId));

        [RelayCommand(CanExecute = nameof(CanChangeSale))]
        private async Task CancelSale()
        {
            if (SelectedItem is null) return;

            if (!_messageService.ShowQuestionMessage("تحذير", MessageTemplates.CancellationDebtConfirmation))
                return;

            var isSuccess = await _service.CancelDebtAsync(SelectedItem.DebtId);

            if (!isSuccess)
            {
                _messageService.ShowErrorMessage("عملية الالغاء", MessageTemplates.CancelDebtErrorMessage);
                return;
            }

            SelectedItem.Status = "ملغات";
            _messageService.ShowInfoMessage("عملية الالغاء", MessageTemplates.CancelDebtSuccessMessage);
        }

        [RelayCommand]
        private void Close() => _windowService.Close();

        private bool CanChangeSale()
        {
            return SelectedItem?.Status == "غير مدفوع";
        }
    }
}
