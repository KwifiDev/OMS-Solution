using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public class ServicesPageViewModel : BasePageViewModel<IServiceService, IServicesSummaryService, ServicesSummaryModel, ServiceModel>
    {
        public ServicesPageViewModel(IServiceService service, IServicesSummaryService displayService,
                                     IDialogService dialogService, IMessageService messageService)
                                     : base(service, displayService, dialogService, messageService)
        {
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

        protected override Task ShowDetailsWindow(int itemId)
        { 
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null) 
            => await _dialogService.ShowDialog<AddEditServiceWindow, int?>(itemId);

        protected override async Task<ServicesSummaryModel> ConvertToModel(ServiceModel messageModel)
            => (await _displayService.GetByIdAsync(messageModel.ServiceId))!;
    }
}