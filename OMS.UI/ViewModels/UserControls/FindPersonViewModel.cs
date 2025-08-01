﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.ViewModels.UserControls.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.ViewModels.UserControls
{
    public partial class FindPersonViewModel : ObservableValidator, IFindPersonViewModel
    {
        public class PersonFoundEventArgs : EventArgs
        {
            public int PersonId { get; }

            public PersonFoundEventArgs(int personId)
            {
                PersonId = personId;
            }
        }
        public event EventHandler<PersonFoundEventArgs>? PersonFound;

        private readonly IPersonService _personService;
        private readonly IMessageService _messageService;

        private string? _personId;

        [ObservableProperty]
        private PersonModel? _person;

        [ObservableProperty]
        private SearchStatus _status;

        public FindPersonViewModel(IPersonService personService, IMessageService messageService, IStatusService statusService)
        {
            _personService = personService;
            _messageService = messageService;

            Status = statusService.CreateSearchStatus();
        }


        [Required(ErrorMessage = "اكتب رقم تعرف الشخص اولاً")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب ان يكون رقم التعريف موجب")]
        public string? PersonId
        {
            get => _personId;
            set => SetProperty(ref _personId, value, true);
        }


        [RelayCommand]
        public async Task FindPerson()
        {
            if (!ValidatePersonId()) return;

            if (!int.TryParse(PersonId, out int id))
            {
                _messageService.ShowErrorMessage("خطأ بحث", MessageTemplates.InvalidNumberMessage);
                return;
            }

            var personModel = await _personService.GetByIdAsync(id);

            if (personModel == null)
            {
                _messageService.ShowErrorMessage("خطأ بحث", MessageTemplates.SearchErrorMessage);
                return;
            }

            Person = personModel;
            Status.IsModifiable = false;
            OnPersonFound();
        }

        private bool ValidatePersonId()
        {
            ValidateAllProperties();
            return !HasErrors;
        }


        private void OnPersonFound()
        {
            PersonFound?.Invoke(this, new PersonFoundEventArgs(Person!.PersonId));
        }

    }
}