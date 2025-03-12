using AutoMapper;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class PeoplePageViewModel : BasePageViewModel<IPersonService, IPersonDetailService, PersonDetailModel, PersonModel>
    {
        public PeoplePageViewModel(IPersonService personService, IPersonDetailService personDetailService, IMapper mapper, IDialogService dialogService,
                                   IMessageService messageService) : base(personService, personDetailService, mapper, dialogService, messageService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(PersonDetailModel item)
            => item.PersonId;

        protected override async Task LoadData()
        {
            var peopleData = await _displayService.GetAllAsync();
            Items = new(_mapper.Map<IEnumerable<PersonDetailModel>>(peopleData));
        }

        protected override async Task ShowDetailsWindow(int itemId)
            => await _dialogService.ShowDialog<PersonDetailsWindow>(itemId);

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditPersonWindow>(itemId);

        protected override async Task<PersonDetailModel> ConvertToModel(PersonModel messageModel)
        {
            var personDto = await _displayService.GetByIdAsync(messageModel.PersonId);
            return _mapper.Map<PersonDetailModel>(personDto);
        }
    }
}
