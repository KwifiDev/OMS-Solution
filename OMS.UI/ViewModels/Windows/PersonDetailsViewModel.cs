using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class PersonDetailsViewModel : ObservableObject, IDialogInitializer
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly IWindowService _windowService;
        [ObservableProperty]
        private PersonModel _person;

        public PersonDetailsViewModel(IPersonService personService, IMapper mapper, IWindowService windowService)
        {
            _personService = personService;
            _mapper = mapper;
            _windowService = windowService;
            _person = new PersonModel();
        }


        public async Task<bool> OnOpeningDialog(int? personId)
        {
            if (personId == null) return false;

            var personDto = await _personService.GetByIdAsync((int)personId);
            if (personDto == null) return false;

            Person = _mapper.Map<PersonModel>(personDto);
            return true;
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }

    }
}
