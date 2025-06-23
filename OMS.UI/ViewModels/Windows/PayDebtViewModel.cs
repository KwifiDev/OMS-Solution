using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class PayDebtViewModel : ObservableObject, IDialogInitializer<int?>
    {
        private readonly IDebtService _debtService;
        private readonly IUserSessionService _userSessionService;
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private PayDebtModel _payDebtModel = null!;

        [ObservableProperty]
        private bool _isModifiable = true;

        public PayDebtViewModel(IDebtService debtService, IUserSessionService userSessionService, IStatusService statusService,
                                IMessageService messageService, IWindowService windowService)
        {
            _debtService = debtService;
            _userSessionService = userSessionService;
            _messageService = messageService;
            _windowService = windowService;
        }

        public async Task<bool> OnOpeningDialog(int? debtId)
        {
            if (debtId is null || debtId < 0) return false;

            var isExist = await _debtService.IsExistAsync((int)debtId);

            if (!isExist) return false;

            PayDebtModel = new PayDebtModel
            {
                DebtId = (int)debtId,
                CreatedByUserId = _userSessionService.CurrentUser!.UserId
            };

            return true;
        }


        [RelayCommand]
        private async Task PayDebt()
        {
            PayDebtModel.PayDebtStatus = await _debtService.PayDebtAsync(PayDebtModel);

            switch (PayDebtModel.PayDebtStatus)
            {
                case EnPayDebtStatus.Success:
                    SendMessage();
                    _messageService.ShowInfoMessage("اجراء دفع", "تم الدفع بنجاح");
                    IsModifiable = false;
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
            WeakReferenceMessenger.Default.Send(PayDebtModel);
        }

    }
}