using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.Common.Data;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Windows.AddEditWindow;

namespace OMS.UI.ViewModels.Windows
{
    public partial class RoleClaimsViewModel : BasePageViewModel<IRoleClaimService, IRoleClaimService, RoleClaimModel, RoleClaimModel>, IDialogInitializer<int?>
    {
        protected override string ViewClaim => PermissionsData.RoleClaims.View;
        protected override string AddClaim => PermissionsData.RoleClaims.Add;
        protected override string EditClaim => string.Empty;
        protected override string DeleteClaim => PermissionsData.RoleClaims.Delete;

        private int _roleId;
        private readonly IWindowService _windowService;
        private readonly IRoleService _roleService;

        public RoleClaimsViewModel(IRoleClaimService service, ILoadingService loadingService, IRoleService roleService,
                                   IDialogService dialogService, IMessageService messageService, IWindowService windowService, IUserSessionService userSessionService)
                                   : base(service, service, loadingService, dialogService, messageService, userSessionService)
        {
            _windowService = windowService;
            _roleService = roleService;
        }

        public async Task<bool> OnOpeningDialog(int? roleId)
        {
            if (roleId is null) return false;
            _roleId = (int)roleId;

            await LoadData();
            return _roleId > 0;
        }


        protected override async Task LoadData()
        {
            await LoadingService.ExecuteWithLoadingIndicator(async () =>
            {
                var items = await _displayService.GetRoleClaimsByRoleIdAsync(_roleId);
                Items = new(items);
            });
        }

        protected override async void OnMessageReceived(object recipient, IMessage<RoleClaimModel> message)
        {
            await LoadData();
        }


        [RelayCommand]
        private async Task Close()
        {
            await SendMassage();
            await _userSessionService.UpdateClaims();
            _windowService.Close();
        }


        private async Task SendMassage()
        {
            var roleModel = await _roleService.GetByIdAsync(_roleId);
            if (roleModel is null) return;

            var message = new ModelTransferService<RoleModel>
            {
                Model = roleModel,
                Status = new AddEditStatus { Operation = AddEditStatus.EnExecuteOperation.Updated }
            };

            WeakReferenceMessenger.Default.Send<IMessage<RoleModel>>(message);
        }

        #region Common Abstract Methods
        protected override Task<RoleClaimModel> ConvertToModel(RoleClaimModel messageModel)
            => Task.FromResult(messageModel);

        protected override async Task<bool> ExecuteDelete(int itemId)
        {
            if (SelectedItem is null) return false;
            return await _service.RemoveRoleClaimAsync(_roleId, SelectedItem.ClaimType!, SelectedItem.ClaimValue!);
        }

        protected override int GetItemId(RoleClaimModel item)
            => 0;

        protected override Task ShowDetailsWindow(int itemId)
            => throw new NotImplementedException();

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditRoleClaimWindow, int?>(_roleId);
        #endregion
    }
}
