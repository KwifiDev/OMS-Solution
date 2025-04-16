using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class ClientsPageViewModel : BasePageViewModel<IClientService, IClientsSummaryService, ClientsSummaryModel, ClientModel>
    {
        public ClientsPageViewModel(IClientService clientService, IClientsSummaryService clientsSummaryService,
                                    IMapper mapper, IDialogService dialogService, IMessageService messageService)
                                    : base(clientService, clientsSummaryService, mapper, dialogService, messageService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
        {
            return await _service.DeleteAsync(itemId);
        }

        protected override int GetItemId(ClientsSummaryModel item)
            => item.ClientId;

        protected override async Task LoadData()
        {
            var clientItems = await _displayService.GetAllAsync();
            Items = new(_mapper.Map<IEnumerable<ClientsSummaryModel>>(clientItems));
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("لم يتم اجراء", "لم يتم انشاء هذه الأضافة بعد");
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditClientWindow>(itemId);

        protected override async Task<ClientsSummaryModel> ConvertToModel(ClientModel messageModel)
        {
            var clientDto = await _displayService.GetByIdAsync(messageModel.ClientId);
            return _mapper.Map<ClientsSummaryModel>(clientDto);
        }

        [RelayCommand]
        private void ShowClientAccountDetails()
        {
            _dialogService.ShowDialog<ClientAccountDetailsWindow>(SelectedItem?.AccountId);
        }
    }
}