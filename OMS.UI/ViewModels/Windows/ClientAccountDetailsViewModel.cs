using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class ClientAccountDetailsViewModel : ObservableObject, IDialogInitializer<int?>
    {
        private readonly IUserAccountService _userAccountService;
        //private readonly IMapper _mapper;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private UserAccountModel _userAccount;

        public ClientAccountDetailsViewModel(IUserAccountService userAccountService, IWindowService windowService)
        {
            _userAccountService = userAccountService;
            //_mapper = mapper;
            _windowService = windowService;
            _userAccount = new UserAccountModel();
        }


        public async Task<bool> OnOpeningDialog(int? accountId)
        {
            if (accountId == null) return false;

            var userAccountModel = await _userAccountService.GetByIdAsync((int)accountId);
            if (userAccountModel == null) return false;

            UserAccount = userAccountModel;
            return true;
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }


    }
}