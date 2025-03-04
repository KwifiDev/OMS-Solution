using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Interfaces;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows
{
    public partial class AddEditPersonViewModel : ObservableObject, IViewModelInitializer
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private PersonModel _person = null!;

        [ObservableProperty]
        private AddEditStatus _status;

        public AddEditPersonViewModel(IPersonService personService, IMapper mapper, IMessageService messageService,
                                      IWindowService windowService, IStatusService statusService)
        {
            Genders = GenderOption.Genders;
            _personService = personService;
            _mapper = mapper;
            _messageService = messageService;
            _windowService = windowService;

            Status = statusService.CreateAddEditStatus();
        }

        public ObservableCollection<GenderOption> Genders { get; }

        public async Task<bool> Initialize(int? personId = -1)
        {
            try
            {
                return personId > 0 ? await RunEditingMode(personId) : RunAddingMode();
            }
            catch (Exception ex)
            {
                _messageService.ShowErrorMessage("خطأ في التهيئة", ex.Message);
                return false;
            }
        }

        private bool RunAddingMode()
        {
            Status.SelectMode = AddEditStatus.EnMode.Add;

            Person = new PersonModel();
            return true;
        }

        private async Task<bool> RunEditingMode(int? personId)
        {
            if (personId == null)
            {
                _messageService.ShowErrorMessage("خطأ", MessageTemplates.ErrorMessage);
                return false;
            }

            var personDto = await _personService.GetByIdAsync((int)personId);
            if (personDto == null)
            {
                _messageService.ShowErrorMessage("اجراء البحث عن شخص", MessageTemplates.SearchErrorMessage);
                return false;
            }

            Status.SelectMode = AddEditStatus.EnMode.Edit;

            Person = _mapper.Map<PersonModel>(personDto);
            return true;
        }

        [RelayCommand]
        private async Task SavePerson(object? parameter)
        {
            if (!ValidatePerson()) return;

            var personDto = MapPersonToDto();
            var isAdding = Status.SelectMode == AddEditStatus.EnMode.Add;

            bool isSuccess = await SavePersonData(isAdding, personDto);

            if (!isSuccess)
            {
                _messageService.ShowErrorMessage("اجراء حفظ بيانات شخص", MessageTemplates.SaveErrorMessage);
                return;
            }

            UpdateStatusAndNotify(isAdding, personDto);

            Status.IsModifiable = false;
            Status.ModelObject = Person;
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }

        [RelayCommand]
        private void DragWindow()
        {
            _windowService.DragMove();
        }

        private bool ValidatePerson()
        {
            if (!Person.ArePropertiesValid())
            {
                _messageService.ShowInfoMessage("تحقق", MessageTemplates.ValidationErrorMessage(Person.GetErrors()?.FirstOrDefault()?.ErrorMessage));
                return false;
            }
            return true;
        }

        private PersonDto MapPersonToDto()
        {
            return _mapper.Map<PersonDto>(Person);
        }

        private async Task<bool> SavePersonData(bool isAdding, PersonDto personDto)
        {
            return isAdding
                ? await _personService.AddAsync(personDto)
                : await _personService.UpdateAsync(personDto);
        }

        private void UpdateStatusAndNotify(bool isAdding, PersonDto personDto)
        {
            if (isAdding)
            {
                Person.PersonId = personDto.PersonId;
                Status.Operation = AddEditStatus.EnExecuteOperation.Added;

                _messageService.ShowInfoMessage("اجراء اضافة شخص جديد", MessageTemplates.AdditionSuccessMessage);
            }
            else
            {
                Status.Operation = AddEditStatus.EnExecuteOperation.Updated;

                _messageService.ShowInfoMessage("اجراء تعديل بيانات شخص", MessageTemplates.UpdateSuccessMessage);
            }

            SendMessage();

        }

        private void SendMessage()
        {
            var message = new ModelTransferService<PersonModel> { Model = Person, Status = Status };
            WeakReferenceMessenger.Default.Send<IMessage<PersonModel>>(message);
        }
    }
}
