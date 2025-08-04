using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Tables;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditRoleClaimViewModel : AddEditBaseViewModel<RoleClaimModel, IRoleClaimService>
    {

        public AddEditRoleClaimViewModel(IRoleClaimService roleClaimService, IMessageService messageService,
                                        IWindowService windowService, IStatusService statusService)
                                        : base(roleClaimService, messageService, windowService, statusService)
        {
            ClaimsTypes = [new("صلاحية", EnRoleClaimTypes.Permission.ToString()),
                           new("مستوى الوصول", EnRoleClaimTypes.AccessLevel.ToString()),
                           new("نطاق الوصول", EnRoleClaimTypes.DataScope.ToString()),
                           new("وحدة الوصول", EnRoleClaimTypes.ModuleAccess.ToString())];
        }

        public record ClaimsTypeOption(string DisplayMember, string Value);
        public ObservableCollection<ClaimsTypeOption> ClaimsTypes { get; }


        public override Task<bool> OnOpeningDialog(int? roleId)
        {
            if (roleId is null) return Task.FromResult(false);
            base.EnterAddMode();

            Model.RoleId = (int)roleId;
            return Task.FromResult(true);
        }

        protected override async Task<RoleClaimModel?> GetByIdAsync(int roleClaimId)
            => await _service.GetByIdAsync(roleClaimId);

        protected override string GetEntityName()
            => "مطالبة";

        protected override async Task<bool> SaveDataAsync(bool isAdding, RoleClaimModel roleClaimModel)
            => isAdding ? await _service.AddRoleClaimAsync(roleClaimModel.RoleId, roleClaimModel.ClaimType!, roleClaimModel.ClaimValue!) : false;
    }
}
