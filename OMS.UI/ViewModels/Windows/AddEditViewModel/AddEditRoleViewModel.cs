using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditRoleViewModel : AddEditBaseViewModel<RoleModel, IRoleService>
    {
        private readonly IUserSessionService _userSessionService;

        private RoleModel? _oldRoleData;

        private bool _isRoleInToken = false;

        public AddEditRoleViewModel(IRoleService roleService, IMessageService messageService,
                                    IWindowService windowService, IStatusService statusService, IUserSessionService userSessionService)
                                    : base(roleService, messageService, windowService, statusService)
        {
            _userSessionService = userSessionService;
        }

        protected override async Task<RoleModel?> GetByIdAsync(int roleId)
            => await _service.GetByIdAsync(roleId);

        protected override string GetEntityName()
            => "دور";

        protected override async Task<bool> SaveDataAsync(bool isAdding, RoleModel roleModel)
        {
            if (isAdding)
            {
                return await _service.AddAsync(roleModel);
            }
            else
            {
                var isRoleUpdated = await _service.UpdateAsync(roleModel.Id, roleModel);

                var isRolesNamesChangedInToken = _isRoleInToken && isRoleUpdated && _oldRoleData?.Name != roleModel.Name;

                if (isRolesNamesChangedInToken)
                {
                    await _userSessionService.UpdateToken();
                }

                return isRoleUpdated;
            }
        }

        protected override async Task<bool> EnterEditModeAsync(int? id)
        {
            var isRoleModelLoaded = await base.EnterEditModeAsync(id);
            if (isRoleModelLoaded)
            {
                _oldRoleData = new RoleModel { Id = Model.Id, Name = Model.Name };

                var userRoles = _userSessionService.GetUserRoles();

                _isRoleInToken = userRoles.Contains(_oldRoleData.Name);
                return true;
            }

            return false;
        }
    }
}
