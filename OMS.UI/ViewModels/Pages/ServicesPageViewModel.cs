using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.Windows;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class ServicesPageViewModel : BasePageViewModel<IServiceService, IServicesSummaryService, ServicesSummaryModel, ServiceModel>
    {
        private readonly IWindowService _windowService;

        public ServicesPageViewModel(IServiceService service, IServicesSummaryService displayService,
                                     IDialogService dialogService, IMessageService messageService, IWindowService windowService)
                                     : base(service, displayService, dialogService, messageService)
        {
            _windowService = windowService;
        }



        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(ServicesSummaryModel item)
            => item.ServiceId;

        protected override async Task LoadData()
        {
            var servicesData = await _displayService.GetAllAsync();
            Items = new(servicesData);
        }

        protected override async Task ShowDetailsWindow(int itemId)
            => await _dialogService.ShowDialog<DiscountsAppliedWindow, int>(itemId);


        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditServiceWindow, int?>(itemId);

        protected override async Task<ServicesSummaryModel> ConvertToModel(ServiceModel messageModel)
            => (await _displayService.GetByIdAsync(messageModel.ServiceId))!;


        [RelayCommand]
        private void Close() => _windowService.Close();
    }
}