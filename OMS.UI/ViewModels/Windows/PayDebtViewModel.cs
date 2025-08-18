using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.Common.Data;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class PayDebtViewModel : ObservableObject, IDialogInitializer<(int Id, DebtStatus.EnMode DebtOperation)>
    {
        private readonly IClientService _clientService;
        private readonly IDebtService _debtService;
        private readonly IUserSessionService _userSessionService;
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private PayDebtModel? _payDebtModel;

        [ObservableProperty]
        private PayDebtsModel? _payDebtsModel;

        [ObservableProperty]
        private DebtStatus _debtStatus;

        [ObservableProperty]
        private string? _notes;

        public PayDebtViewModel(IClientService clientService, IDebtService debtService, IUserSessionService userSessionService,
                                IStatusService statusService, IMessageService messageService, IWindowService windowService)
        {
            _clientService = clientService;
            _debtService = debtService;
            _userSessionService = userSessionService;
            _messageService = messageService;
            _windowService = windowService;

            DebtStatus = statusService.CreateDebtStatus();
        }

        public async Task<bool> OnOpeningDialog((int Id, DebtStatus.EnMode DebtOperation) parameters) =>
            parameters.DebtOperation switch
            {
                DebtStatus.EnMode.PaySpecifiecDebt => await SetSpecificDebtMode(parameters.Id),
                DebtStatus.EnMode.PayAllDebtsByClientId => await SetPayAllDebtsMode(parameters.Id),
                _ => false
            };

        private async Task<bool> SetSpecificDebtMode(int debtId)
        {
            if (debtId <= 0 || !await _debtService.IsExistAsync(debtId)) return false;

            DebtStatus.SelectMode = DebtStatus.EnMode.PaySpecifiecDebt;
            PayDebtModel = new PayDebtModel
            {
                DebtId = debtId,
                CreatedByUserId = _userSessionService.CurrentUser!.UserId
            };

            return true;
        }

        private async Task<bool> SetPayAllDebtsMode(int clientId)
        {
            if (clientId <= 0 || !await _clientService.IsExistAsync(clientId)) return false;

            DebtStatus.SelectMode = DebtStatus.EnMode.PayAllDebtsByClientId;
            PayDebtsModel = new PayDebtsModel
            {
                ClientId = clientId,
                CreatedByUserId = _userSessionService.CurrentUser!.UserId
            };

            return true;
        }

        [RelayCommand(CanExecute = nameof(CanPayDebt))]
        private async Task PayDebt()
        {
            switch (DebtStatus.SelectMode)
            {
                case DebtStatus.EnMode.PaySpecifiecDebt:
                    await HandleSingleDebtPayment();
                    break;

                case DebtStatus.EnMode.PayAllDebtsByClientId:
                    await HandleAllDebtsPayment();
                    break;
            }
        }

        private bool CanPayDebt() 
        {
            return _userSessionService.Claims!.Contains(PermissionsData.Debts.Pay);
        }

        private async Task HandleSingleDebtPayment()
        {
            if (PayDebtModel is null) return;

            PayDebtModel.Notes = Notes;
            PayDebtModel.PayDebtStatus = await _debtService.PayDebtAsync(PayDebtModel);
            NotifyUserOfResult(PayDebtModel.PayDebtStatus);
        }

        private async Task HandleAllDebtsPayment()
        {
            if (PayDebtsModel is null) return;

            PayDebtsModel.Notes = Notes;
            PayDebtsModel.PayDebtStatus = await _clientService.PayAllDebtsById(PayDebtsModel);
            NotifyUserOfResult(PayDebtsModel.PayDebtStatus);
        }

        private void NotifyUserOfResult(EnPayDebtStatus status)
        {
            switch (status)
            {
                case EnPayDebtStatus.Success:
                    _messageService.ShowInfoMessage("اجراء دفع", "تم الدفع بنجاح");
                    DebtStatus.Operation = DebtStatus.EnExecuteOperation.FullPaid;
                    SendMessage();
                    break;

                case EnPayDebtStatus.InsufficientBalance:
                    _messageService.ShowInfoMessage("اجراء دفع", "لا يوجد رصيد كافي");
                    break;

                default:
                    _messageService.ShowErrorMessage("اجراء دفع", "حدث خطأ اثناء الدفع");
                    break;
            }
        }

        private void SendMessage()
        {
            if (DebtStatus.SelectMode == DebtStatus.EnMode.PaySpecifiecDebt && PayDebtModel is not null)
                WeakReferenceMessenger.Default.Send(PayDebtModel);
        }

        [RelayCommand]
        private void Close() => _windowService.Close();
    }
}
