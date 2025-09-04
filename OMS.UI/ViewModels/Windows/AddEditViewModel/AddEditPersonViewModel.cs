using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditPersonViewModel : AddEditBaseViewModel<PersonModel, IPersonService>
    {
        private readonly IUserSessionService _userSessionService;


        public AddEditPersonViewModel(IPersonService personService, IMessageService messageService,
                                      IWindowService windowService, IStatusService statusService, IUserSessionService userSessionService)
                                      : base(personService, messageService, windowService, statusService)
        {
            Genders = [new("ذكر", EnGender.Male), new("انثى", EnGender.Female)];
            _userSessionService = userSessionService;
        }

        public record GenderOption(string DisplayMember, EnGender Value);
        public ObservableCollection<GenderOption> Genders { get; }


        protected override async Task<PersonModel?> GetByIdAsync(int personId)
            => await _service.GetByIdAsync(personId);

        protected override string GetEntityName()
            => "شخص";

        protected override async Task<bool> SaveDataAsync(bool isAdding, PersonModel personModel)
            => isAdding ? await _service.AddAsync(personModel) : await _service.UpdateAsync(personModel.Id, personModel);

        protected override void SendMessage()
        {
            base.SendMessage();
            if (_userSessionService.CurrentUser?.PersonId == Model.Id)
                _userSessionService.UpdateModel();
        }
    }
}
