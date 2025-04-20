using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.BL.IServices.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class ClientAccountDetailsViewModel : ObservableObject, IDialogInitializer<int?>
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IMapper _mapper;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private UserAccountModel _userAccount;

        public ClientAccountDetailsViewModel(IUserAccountService userAccountService, IMapper mapper, IWindowService windowService)
        {
            _userAccountService = userAccountService;
            _mapper = mapper;
            _windowService = windowService;
            _userAccount = new UserAccountModel();
        }


        public async Task<bool> OnOpeningDialog(int? accountId)
        {
            if (accountId == null) return false;

            var userAccountDto = await _userAccountService.GetByIdAsync((int)accountId);
            if (userAccountDto == null) return false;

            UserAccount = _mapper.Map<UserAccountModel>(userAccountDto);
            return true;
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }


    }
}