using CommunityToolkit.Mvvm.Input;
using OMS.Common.Data;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Views;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class RolesSummaryViewModel : BasePageViewModel<IRoleService, IRolesSummaryService, RolesSummaryModel, RoleModel>, IDialogInitializer<int?>
    {
        protected override string ViewClaim => PermissionsData.RoleClaims.View;
        protected override string AddClaim => PermissionsData.Roles.Add;
        protected override string EditClaim => PermissionsData.Roles.Edit;
        protected override string DeleteClaim => PermissionsData.Roles.Delete;


        private readonly IWindowService _windowService;

        public RolesSummaryViewModel(IRoleService service, IRolesSummaryService displayService, ILoadingService loadingService,
                                     IDialogService dialogService, IMessageService messageService, IUserSessionService userSessionService, IWindowService windowService)
                                     : base(service, displayService, loadingService, dialogService, messageService, userSessionService)
        {
            _windowService = windowService;
        }

        public async Task<bool> OnOpeningDialog(int? parameters)
        {
            await LoadData();
            return Items.Any();
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
        {
            var userRoles = _userSessionService.GetUserRoles();
            var selectedRoleName = SelectedItem!.RoleName;

            var isRoleRemoved = await _service.DeleteAsync(itemId);

            var isRolesChangedInToken = userRoles.Contains(selectedRoleName) && isRoleRemoved;

            if (isRolesChangedInToken)
            {
                await _userSessionService.UpdateClaims();
                await _userSessionService.UpdateToken();
            }

            return isRoleRemoved;
        }

        protected override async Task<RolesSummaryModel> ConvertToModel(RoleModel messageModel)
            => (await _displayService.GetByIdAsync(messageModel.Id))!;

        protected override int GetItemId(RolesSummaryModel item) => item.RoleId;

        protected override async Task ShowDetailsWindow(int itemId)
        {
            await _dialogService.ShowDialog<RoleClaimsWindow, int?>(itemId);
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditRoleWindow, int?>(itemId);

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }
    }
}
