using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class PersonDetailsViewModel : ObservableObject, IDialogInitializer<int?>
    {
        private readonly IPersonService _personService;
        //private readonly IMapper _mapper;
        private readonly IWindowService _windowService;
        [ObservableProperty]
        private PersonModel _person;

        public PersonDetailsViewModel(IPersonService personService, IWindowService windowService)
        {
            _personService = personService;
            //_mapper = mapper;
            _windowService = windowService;
            _person = new PersonModel();
        }


        public async Task<bool> OnOpeningDialog(int? personId)
        {
            if (personId == null) return false;

            var personModel = await _personService.GetByIdAsync((int)personId);
            if (personModel == null) return false;

            Person = personModel;
            return true;
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }

    }
}
