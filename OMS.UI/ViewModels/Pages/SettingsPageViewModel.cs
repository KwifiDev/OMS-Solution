using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Registry;
using OMS.UI.Services.Settings;
using OMS.UI.Services.ShowMassage;

namespace OMS.UI.ViewModels.Pages
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        private readonly IRegistryService _registrySerivce;
        private readonly ISettingsService _settingsService;
        private readonly IMessageService _messageService;


        [ObservableProperty]
        private PersonalAppInfoModel _personalAppInfoModel;


        public SettingsPageViewModel(IRegistryService registrySerivce, ISettingsService settingsService, IMessageService messageService)
        {
            _registrySerivce = registrySerivce;
            _settingsService = settingsService;
            _messageService = messageService;
            PersonalAppInfoModel = new();
        }

        [RelayCommand]
        private void LoadData()
        {
            PersonalAppInfoModel.CompanyName = _registrySerivce.GetRegistryValue(nameof(PersonalAppInfoModel.CompanyName), RegistryService.SubRegistryPath.AppConfig);
            PersonalAppInfoModel.Description = _registrySerivce.GetRegistryValue(nameof(PersonalAppInfoModel.Description), RegistryService.SubRegistryPath.AppConfig);

        }

        [RelayCommand]
        private void Save()
        {
            if (!PersonalAppInfoModel.ArePropertiesValid()) return;

            _registrySerivce.SetRegistryValue(nameof(PersonalAppInfoModel.CompanyName), PersonalAppInfoModel.CompanyName!, RegistryService.SubRegistryPath.AppConfig);
            _registrySerivce.SetRegistryValue(nameof(PersonalAppInfoModel.Description), PersonalAppInfoModel.Description!, RegistryService.SubRegistryPath.AppConfig);

            _messageService.ShowInfoMessage("معلومات", MessageTemplates.SaveSuccessMessage);
        }

        [RelayCommand]
        private void OpenServerConfig()
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
        }

        [RelayCommand]
        private void OpenSecurityConfig()
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
        }

        [RelayCommand]
        private void OpenRestoreAndBackupDb()
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
        }
    }
}