using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
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

        public PayDebtViewModel(IClientService clientService, IDebtService debtService, IUserSessionService userSessionService, IStatusService statusService,
                                IMessageService messageService, IWindowService windowService)
        {
            _clientService = clientService;
            _debtService = debtService;
            _userSessionService = userSessionService;
            _messageService = messageService;
            _windowService = windowService;
            DebtStatus = statusService.CreateDebtStatus();
        }

        public async Task<bool> OnOpeningDialog((int Id, DebtStatus.EnMode DebtOperation) parameters)
        {
            switch (parameters.DebtOperation)
            {
                case DebtStatus.EnMode.PaySpecifiecDebt:
                    return await SetPaySpecifiecDebtMode(parameters.Id);

                case DebtStatus.EnMode.PayAllDebtsByClientId:
                    return await SetPayAllDebtsByClientIdMode(parameters.Id);

                default: return false;
            }
        }

        private async Task<bool> SetPaySpecifiecDebtMode(int debtId)
        {
            if (debtId <= 0) return false;

            bool isExist = await _debtService.IsExistAsync(debtId);
            if (!isExist) return false;

            DebtStatus.SelectMode = DebtStatus.EnMode.PaySpecifiecDebt;

            PayDebtModel = new PayDebtModel
            {
                DebtId = debtId,
                CreatedByUserId = _userSessionService.CurrentUser!.UserId
            };

            return true;
        }

        private async Task<bool> SetPayAllDebtsByClientIdMode(int clientId)
        {
            if (clientId <= 0) return false;

            bool isExist1 = await _clientService.IsExistAsync(clientId);
            if (!isExist1) return false;

            DebtStatus.SelectMode = DebtStatus.EnMode.PayAllDebtsByClientId;

            PayDebtsModel = new PayDebtsModel
            {
                ClientId = clientId,
                CreatedByUserId = _userSessionService.CurrentUser!.UserId
            };

            return true;
        }


        [RelayCommand]
        private async Task PayDebt()
        {
            switch (DebtStatus.SelectMode)
            {
                case DebtStatus.EnMode.PaySpecifiecDebt:
                    PayDebtModel!.Notes = Notes;
                    PayDebtModel.PayDebtStatus = await _debtService.PayDebtAsync(PayDebtModel);

                    UpdateAndNotifyStatus(PayDebtModel.PayDebtStatus);
                    break;

                case DebtStatus.EnMode.PayAllDebtsByClientId:
                    PayDebtsModel!.Notes = Notes;
                    PayDebtsModel.PayDebtStatus = await _clientService.PayAllDebtsById(PayDebtsModel);

                    UpdateAndNotifyStatus(PayDebtsModel.PayDebtStatus);
                    break;
            }
        }

        private void UpdateAndNotifyStatus(EnPayDebtStatus payDebtStatus)
        {
            switch (payDebtStatus)
            {
                case EnPayDebtStatus.Success:
                    SendMessage();
                    _messageService.ShowInfoMessage("اجراء دفع", "تم الدفع بنجاح");
                    DebtStatus.Operation = DebtStatus.EnExecuteOperation.FullPaid;
                    break;

                case EnPayDebtStatus.InsufficientBalance:
                    _messageService.ShowInfoMessage("اجراء دفع", "لا يوجد رصيد كافي");
                    break;

                default:
                    _messageService.ShowErrorMessage("اجراء دفع", "حدث خطأ اثناء الدفع");
                    break;
            }

        }

        [RelayCommand]
        private void Close() => _windowService.Close();

        private void SendMessage()
        {
            if (DebtStatus.SelectMode == DebtStatus.EnMode.PaySpecifiecDebt)
                WeakReferenceMessenger.Default.Send(PayDebtModel!);
        }


    }
}