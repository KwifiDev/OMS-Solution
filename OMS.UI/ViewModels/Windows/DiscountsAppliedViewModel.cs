using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class DiscountsAppliedViewModel : BasePageViewModel<IDiscountService, IDiscountsAppliedService, DiscountsAppliedModel, DiscountModel>, IDialogInitializer<int>
    {
        private readonly IWindowService _windowService;
        private int _serviceId;

        public DiscountsAppliedViewModel(IDiscountService service, IDiscountsAppliedService displayService, ILoadingService loadingService,
                                         IDialogService dialogService, IMessageService messageService, IWindowService windowService)
                                         : base(service, displayService, loadingService, dialogService, messageService)
        {
            _windowService = windowService;
        }

        public async Task<bool> OnOpeningDialog(int serviceId)
        {
            if (serviceId <= 0) return false;

            _serviceId = serviceId;
            await LoadData();

            return true;
        }

        protected override async Task<DiscountsAppliedModel> ConvertToModel(DiscountModel messageModel)
            => (await _displayService.GetByIdAsync(messageModel.DiscountId))!;

        protected override async Task<bool> ExecuteDelete(int itemId) => await _service.DeleteAsync(itemId);

        protected override int GetItemId(DiscountsAppliedModel item) => item.DiscountId;

        protected override async Task LoadData()
        {
            await LoadingService.ExecuteWithLoadingIndicator(async () =>
            {
                var discountsAppliedItems = await _displayService.GetDiscountsByServiceIdAsync(_serviceId);
                Items = new(discountsAppliedItems);
            });
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditDiscountWindow, (int? DiscountId, int ServiceId)>((itemId, _serviceId));

        [RelayCommand]
        private void Close() => _windowService.Close();


        // This Method is Disabled
        protected override Task ShowDetailsWindow(int itemId) => Task.CompletedTask;
    }
}