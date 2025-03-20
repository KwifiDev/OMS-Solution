using AutoMapper;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditPersonViewModel : AddEditBaseViewModel<PersonModel, PersonDto, IPersonService>
    {
        private readonly IUserSessionService _userSessionService;


        public AddEditPersonViewModel(IPersonService personService, IMapper mapper, IMessageService messageService,
                                      IWindowService windowService, IStatusService statusService, IUserSessionService userSessionService)
                                      : base(personService, mapper, messageService, windowService, statusService)
        {
            Genders = GenderOption.Genders;
            _userSessionService = userSessionService;
        }

        public ObservableCollection<GenderOption> Genders { get; }


        protected override async Task<PersonDto?> GetByIdAsync(int personId)
            => await _service.GetByIdAsync(personId);

        protected override string GetEntityName()
            => "شخص";

        protected override async Task<bool> SaveDataAsync(bool isAdding, PersonDto personDto)
            => isAdding ? await _service.AddAsync(personDto) : await _service.UpdateAsync(personDto);

        protected override void UpdateModelAfterSave(PersonDto personDto)
            => Model.PersonId = personDto.PersonId;

        protected override void SendMessage()
        {
            base.SendMessage();
            if (_userSessionService.CurrentUser?.PersonId == Model.PersonId)
                _userSessionService.UpdateModel();
        }
    }
}
