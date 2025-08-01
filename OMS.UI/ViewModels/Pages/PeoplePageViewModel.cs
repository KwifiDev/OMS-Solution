﻿using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Views;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class PeoplePageViewModel : BasePageViewModel<IPersonService, IPersonDetailService, PersonDetailModel, PersonModel>
    {
        public PeoplePageViewModel(IPersonService personService, IPersonDetailService personDetailService, ILoadingService loadingService,
                                   IDialogService dialogService, IMessageService messageService)
                                   : base(personService, personDetailService, loadingService, dialogService, messageService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(PersonDetailModel item)
            => item.PersonId;

        protected override async Task ShowDetailsWindow(int itemId)
            => await _dialogService.ShowDialog<PersonDetailsWindow, int?>(itemId);

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditPersonWindow, int?>(itemId);

        protected override async Task<PersonDetailModel> ConvertToModel(PersonModel messageModel)
        {
            return (await _displayService.GetByIdAsync(messageModel.PersonId))!;
        }
    }
}
