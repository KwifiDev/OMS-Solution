using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class SalesSummaryViewModel : BasePageViewModel<ISaleService, ISalesSummaryService, SalesSummaryModel, SaleModel>, IDialogInitializer<int?>
    {
        private readonly IWindowService _windowService;
        private int _clientId;

        public SalesSummaryViewModel(ISaleService service, ISalesSummaryService displayService, IDialogService dialogService,
                                     IMessageService messageService, IWindowService windowService) : base(service, displayService, dialogService, messageService)
        {
            _windowService = windowService;
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
            var salesData = await _displayService.GetSalesByClientIdAsync(_clientId);
            Items = new(salesData);
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            throw new NotImplementedException();
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditSaleWindow, (int? SaleId, int ClientId)>((itemId, _clientId));

        [RelayCommand]
        private void Close() => _windowService.Close();
    }
}
