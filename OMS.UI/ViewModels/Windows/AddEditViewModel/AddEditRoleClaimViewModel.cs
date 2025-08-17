using CommunityToolkit.Mvvm.ComponentModel;
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

        private readonly IPermissionService _permissionService;

        [ObservableProperty]
        private ObservableCollection<PermissionModel> _claimsValues = new(Enumerable.Empty<PermissionModel>());


        public AddEditRoleClaimViewModel(IRoleClaimService roleClaimService, IMessageService messageService,
                                        IWindowService windowService, IStatusService statusService, IPermissionService permissionService)
                                        : base(roleClaimService, messageService, windowService, statusService)
        {
            _permissionService = permissionService;

            ClaimsTypes = [new("صلاحية", EnRoleClaimTypes.Permission.ToString())];
            //new("مستوى الوصول", EnRoleClaimTypes.AccessLevel.ToString()),
            //new("نطاق الوصول", EnRoleClaimTypes.DataScope.ToString()),
            //new("وحدة الوصول", EnRoleClaimTypes.ModuleAccess.ToString())];
        }

        public record ClaimsTypeOption(string DisplayMember, string Value);

        public ObservableCollection<ClaimsTypeOption> ClaimsTypes { get; }


        public override async Task<bool> OnOpeningDialog(int? roleId)
        {
            if (roleId is null) return false;
            base.EnterAddMode();

            Model.ClaimType = ClaimsTypes.Select(cto => cto.Value).FirstOrDefault();
            Model.RoleId = (int)roleId;

            await LoadPermissionsData();

            return true;
        }

        private async Task<bool> LoadPermissionsData()
        {
            ClaimsValues = new(await _permissionService.GetAllAsync());
            return ClaimsValues.Count > 0;
        }

        protected override async Task<RoleClaimModel?> GetByIdAsync(int roleClaimId)
            => await _service.GetByIdAsync(roleClaimId);

        protected override string GetEntityName()
            => "مطالبة";

        protected override async Task<bool> SaveDataAsync(bool isAdding, RoleClaimModel roleClaimModel)
            => isAdding ? await _service.AddRoleClaimAsync(roleClaimModel.RoleId, roleClaimModel.ClaimType!, roleClaimModel.ClaimValue!) : false;
    }
}
