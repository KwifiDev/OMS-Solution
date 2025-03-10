using AutoMapper;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class PeoplePageViewModel : BasePageViewModel<IPersonService, PersonModel>
    {
        public PeoplePageViewModel(IPersonService personService, IMapper mapper, IDialogService dialogService,
                                   IMessageService messageService) : base(personService, mapper, dialogService, messageService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(PersonModel item)
            => item.PersonId;

        protected override async Task LoadData()
        {
            var peopleData = await _service.GetAllAsync();
            Items = new(_mapper.Map<IEnumerable<PersonModel>>(peopleData));
        }

        protected override async Task ShowDetailsWindow(int itemId)
            => await _dialogService.ShowDialog<PersonDetailsWindow>(itemId);

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditPersonWindow>(itemId);
    }
}
