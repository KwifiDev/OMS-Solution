using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.Models.Others;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Registry;
using OMS.UI.Services.Settings;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        private readonly IRegistryService _registrySerivce;
        private readonly ISettingsService _settingsService;
        private readonly IMessageService _messageService;
        private readonly IDialogService _dialogService;


        [ObservableProperty]
        private PersonalAppInfoModel _personalAppInfoModel;


        public SettingsPageViewModel(IRegistryService registrySerivce, ISettingsService settingsService, IMessageService messageService,
                                     IDialogService dialogService)
        {
            _registrySerivce = registrySerivce;
            _settingsService = settingsService;
            _messageService = messageService;
            _dialogService = dialogService;
            PersonalAppInfoModel = new();
        }

        [RelayCommand]
        private void LoadData()
        {
            _registrySerivce.GetAppConfig(out string? companyName, out string? description);
            PersonalAppInfoModel.CompanyName = companyName;
            PersonalAppInfoModel.Description = description;

        }

        [RelayCommand]
        private void Save()
        {
            if (!PersonalAppInfoModel.ArePropertiesValid()) return;

            _registrySerivce.SetAppConfig(PersonalAppInfoModel.CompanyName!, PersonalAppInfoModel.Description!);

            _messageService.ShowInfoMessage("معلومات", MessageTemplates.SaveSuccessMessage);
        }

        [RelayCommand]
        private void OpenServerConfig()
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
        }

        [RelayCommand]
        private async Task OpenRolesConfig()
        {
            await _dialogService.ShowDialog<RolesSummaryWindow, int?>();
        }

        [RelayCommand]
        private void OpenRestoreAndBackupDb()
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
        }
    }
}