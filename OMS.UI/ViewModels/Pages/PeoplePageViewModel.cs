using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.Status;
using OMS.UI.ViewModels.Windows;
using OMS.UI.Views.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Pages
{
    public partial class PeoplePageViewModel : ObservableObject
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly IDialogService _dialogService;
        private readonly IMessageService _messageService;

        [ObservableProperty]
        private ObservableCollection<PersonModel>? _people;

        [ObservableProperty]
        private PersonModel? _selectedPerson;

        public PeoplePageViewModel(IPersonService personService, IMapper mapper, IDialogService dialogService, IMessageService messageService)
        {
            _personService = personService;
            _mapper = mapper;
            _dialogService = dialogService;
            _messageService = messageService;

            WeakReferenceMessenger.Default.Register<IMessage<PersonModel>>(this, OnMessageReceived);
        }

        private void OnMessageReceived(object recipient, IMessage<PersonModel> message)
        {
            switch (message.Status.Operation)
            {
                case StatusInfo.EnExecuteOperation.Added:
                    OnPersonAdd(message.Model);
                    break;
                case StatusInfo.EnExecuteOperation.Updated:
                    OnPersonEdit(message.Model);
                    break;
                default:
                    // Handle other cases if needed
                    break;
            }
        }

        [RelayCommand]
        private async Task ShowDetails(PersonModel? person)
        {
            if (person == null) return;
            SelectItem(person);

            await _dialogService.ShowDialog<PersonDetailsWindow, PersonDetailsViewModel>(person.PersonId);
        }

        //[RelayCommand]
        //private async Task AddPerson()
        //{
        //    await _dialogService.ShowDialog<WDAddEditPerson, VMAddEditPerson>();
        //}

        //[RelayCommand]
        //private async Task EditPerson(PersonModel? person)
        //{
        //    if (person == null) return;
        //    SelectItem(person);

        //    await _dialogService.ShowDialog<WDAddEditPerson, VMAddEditPerson>(person.PersonId);
        //}

        [RelayCommand]
        private async Task DeletePerson(PersonModel? person)
        {
            if (person == null) return;
            SelectItem(person);

            if (!_messageService.ShowQuestionMessage("تحذير", MessageTemplates.DeletionConfirmation))
                return;

            bool isDeleted = await _personService.DeleteAsync(person.PersonId);
            if (isDeleted)
            {
                People!.Remove(SelectedPerson!);
                _messageService.ShowInfoMessage("اجراء حذف", MessageTemplates.DeletionSuccessMessage);
            }
            else
            {
                _messageService.ShowErrorMessage("خطأ", MessageTemplates.DeletionErrorMessage);
            }
        }

        private void OnPersonAdd(PersonModel? person)
        {
            if (person == null) return;
            PersonModel? personModel = person as PersonModel;

            People!.Add(personModel!);
        }

        private void OnPersonEdit(PersonModel? person)
        {
            if (person == null) return;
            PersonModel? personModel = person as PersonModel;

            int personIndex = People!.IndexOf(SelectedPerson!);
            People[personIndex] = personModel!;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            var peopleDto = await _personService.GetAllAsync();
            var people = _mapper.Map<IEnumerable<PersonModel>>(peopleDto);
            People = new ObservableCollection<PersonModel>(people ?? Enumerable.Empty<PersonModel>());
        }

        private void SelectItem(PersonModel person)
        {
            SelectedPerson = person;
        }
    }
}
