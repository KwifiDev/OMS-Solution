using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditRoleViewModel : AddEditBaseViewModel<RoleModel, IRoleService>
    {

        public AddEditRoleViewModel(IRoleService roleService, IMessageService messageService,
                                    IWindowService windowService, IStatusService statusService)
                                    : base(roleService, messageService, windowService, statusService)
        {
        }

        protected override async Task<RoleModel?> GetByIdAsync(int roleId)
            => await _service.GetByIdAsync(roleId);

        protected override string GetEntityName()
            => "دور";

        protected override async Task<bool> SaveDataAsync(bool isAdding, RoleModel roleModel)
            => isAdding ? await _service.AddAsync(roleModel) : await _service.UpdateAsync(roleModel.Id, roleModel);
    }
}
